using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaminaTraining : MonoBehaviour
{
    public GameObject lizard;
    public List<GameObject> flies = new List<GameObject>();

    public GameObject flyPrefab;
    public GameManager manager;

    public bool isMoving = false;
    public bool isExtending = false;

    public float tongueSpeed = 5f;
    public float rotateSpeed = 5f;
    public int curFly = 0;

    private Vector3 tongueStart;
    private Rigidbody lizRb;
    private Quaternion rotateAmount;
    private Quaternion lookDir;

    // Start is called before the first frame update
    void Start()
    {
        tongueStart = transform.position;
        lizRb = lizard.GetComponent<Rigidbody>();
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flies.Count > 0)
        {
            Vector3 lookPos = flies[curFly].transform.position - lizard.transform.position;
            lookPos.z = 0;
            lookDir = Quaternion.LookRotation(lookPos, new Vector3(0, 0, -90));
            rotateAmount = Quaternion.Slerp(lizard.transform.rotation, lookDir, Time.deltaTime * 2);
            lizard.transform.rotation = rotateAmount;

            isMoving = true;
            
        }
        ExtendTongue();
        RetractTongue();
    }

    public void SpawnFly()
    {
        if(manager.money >= 15)
        {
            manager.money -= 15;
            flies.Add(Instantiate(flyPrefab, new Vector3(Random.Range(-8, 8), Random.Range(2, 5)), Quaternion.Euler(0, 0, 0)));
            isExtending = true;
            isMoving = true;
        }
    }

    public void ExtendTongue()
    {
        if (isExtending && isMoving && Quaternion.Angle(rotateAmount, lookDir) < 5)
        {
            transform.Translate(Vector3.up * tongueSpeed * Time.deltaTime);
        }
    }

    public void RetractTongue()
    {
        if (!isExtending && isMoving && lizRb.angularVelocity.magnitude < 0.1)
        {
            transform.Translate(Vector3.down * tongueSpeed * Time.deltaTime);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("fly") && other.gameObject == flies[curFly])
        {
            Destroy(other.gameObject);
            manager.maxPlayerStamina += 0.05f;
            isExtending = false;
            curFly++;
            if(curFly == flies.Count)
            {
                flies.Clear();
                flies.TrimExcess();
                curFly = 0;
            }
        }
        else if (other.CompareTag("Landing"))
        {
            isExtending = true;
            if (flies.Count == 0)
            {
                isMoving = false;
            }
        }
        else if (other.CompareTag("red"))
        {
            isExtending = false;
        }
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene(1);
    }
}
