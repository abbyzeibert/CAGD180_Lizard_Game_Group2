using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Abby Zeibert
 * 12/08/2025
 * Handles stamina training minigame
 */

public class StaminaTraining : MonoBehaviour
{
    //game objects
    public GameObject lizard;
    public List<GameObject> flies = new List<GameObject>();
    public GameObject flyPrefab;
    public GameManager manager;
    public TMP_Text money;
    public TMP_Text stamina;

    //game state variables
    public bool isMoving = false;
    public bool isExtending = false;

    //lizard stat variables
    public float tongueSpeed = 5f;
    public float rotateSpeed = 5f;
    public int curFly = 0;

    //rotation variables
    private Quaternion rotateAmount;
    private Quaternion lookDir;

    // Start is called before the first frame update
    void Start()
    {
        //initilizes values, finds game manager and updates stamina and money text from it
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        money.text = "Money: " + manager.money;
        stamina.text = "Stamina: " + (Mathf.Round(manager.maxPlayerStamina * 100));
    }

    // Update is called once per frame
    void Update()
    {
        //when there is at least one fly, rotates lizard to face the currently targeted fly
        if (flies.Count > 0)
        {
            //calculates distance between target and lizard, sets z coordinate to 0 to prevent
            //rotation along x and y axis
            Vector3 lookPos = flies[curFly].transform.position - lizard.transform.position;
            lookPos.z = 0;

            //calculates rotation to make lizard look at fly and smoothly rotates lizard there
            lookDir = Quaternion.LookRotation(lookPos, new Vector3(0, 0, -90));
            rotateAmount = Quaternion.Slerp(lizard.transform.rotation, lookDir, Time.deltaTime * 2);
            lizard.transform.rotation = rotateAmount;

            //sets tongue movement to true
            isMoving = true;
            
        }
        //tongue movement, activates when given state is true
        ExtendTongue();
        RetractTongue();
    }

    /// <summary>
    /// Spawns a fly in a random position in the area at the top of the screen and takes money from player
    /// </summary>
    public void SpawnFly()
    {
        if(manager.money >= 15)
        {
            manager.money -= 15;
            money.text = "Money: " + manager.money;
            flies.Add(Instantiate(flyPrefab, new Vector3(Random.Range(-8, 8), Random.Range(2, 5)), Quaternion.Euler(0, 0, 0)));

            //sets tonge movement and extending to true to initiate eating
            isExtending = true;
            isMoving = true;
        }
    }

    /// <summary>
    /// Performs extending the lizard's tongue
    /// </summary>
    public void ExtendTongue()
    {
        //when lizard is close to facing current fly and correct state is active, 
        //moves the tongue forward
        if (isExtending && isMoving && Quaternion.Angle(rotateAmount, lookDir) < 5)
        {
            transform.Translate(Vector3.up * tongueSpeed * Time.deltaTime);
        }
    }

    /// <summary>
    /// Performs retracting the lizard's tongue
    /// </summary>
    public void RetractTongue()
    {
        //when correct state is active, moves the tongue back
        if (!isExtending && isMoving)
        {
            transform.Translate(Vector3.down * tongueSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //when tongue hits the current fly, destroys it and increases player stamina
        if (other.CompareTag("fly") && other.gameObject == flies[curFly])
        {
            Destroy(other.gameObject);
            manager.maxPlayerStamina += 0.05f;
            stamina.text = "Stamina: " + (Mathf.Round(manager.maxPlayerStamina * 100));

            //changes tongue state to begin retracting
            isExtending = false;

            //moves to next fly in list and clears list of flies if none are left
            curFly++;
            if(curFly == flies.Count)
            {
                flies.Clear();
                flies.TrimExcess();
                curFly = 0;
            }
        }
        //used to stop retracting tongue when it hits the walls behind the lizard
        else if (other.CompareTag("Landing"))
        {
            isExtending = true;
            //stops moving tongue if no flies are left
            if (flies.Count == 0)
            {
                isMoving = false;
            }
        }
        //used to stop extending tongue when it hits the walls in front of the lizard, 
        //in case it misses the fly
        else if (other.CompareTag("red"))
        {
            isExtending = false;
        }
    }

    /// <summary>
    /// Loads hub scene
    /// </summary>
    public void ReturnToHub()
    {
        SceneManager.LoadScene(1);
    }
}
