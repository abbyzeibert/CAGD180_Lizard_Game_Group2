using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/*
 * Abby Zeibert
 * 12/04/2025
 * Handles leap training minigame
 */

public class LeapTraining : MonoBehaviour
{
    //game state variables
    public bool gameRunning = false;
    public bool justJumped = false;

    //score variables
    public int leapToAdd = 0;
    public int numJumps = 0;

    //jump indicator variables
    public float markerSpeed = 5;
    private float markDistance;
    public float maxDistance = 15;
    public int direction = 1;
    public GameObject markStart;

    //game objects
    public Rigidbody lizard;
    public GameObject marker;
    public GameManager manager;

    //UI elements
    public TMP_Text intro;
    public TMP_Text timer;
    public TMP_Text result;

    // Start is called before the first frame update
    void Start()
    {
        //finds game manager and starts game start countdown
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update()
    {
        //Only moves marker and detects key presses if race is active and player has not just cheered
        if (gameRunning && !justJumped)
        {
            //calculates distance marker is away from its start point
            markDistance = Vector3.Distance(markStart.transform.position, marker.transform.position);

            //turns marker around when it reaches the edges of its box
            if(markDistance >= maxDistance)
            {
                direction = -1;
            }
            else if(markDistance <= 0.01)
            {
                direction = 1;
            }

            //moves the marker each frame
            marker.transform.position += Vector3.right * markerSpeed * direction * Time.deltaTime;
        }


        if(gameRunning && Input.GetKeyDown(KeyCode.Space) && !justJumped)
        {
            //adds a different score based on where the marker was when player jumped
            //adds variable force to lizard to cosmetically indicate how far they went
            //but lizard's actual position is not used for score
            if(markDistance > 12)
            {
                numJumps += 5;
                lizard.AddForce(Vector3.right * 8, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 4, ForceMode.Impulse);
            }
            else if(markDistance > 8)
            {
                numJumps += 2;
                lizard.AddForce(Vector3.right * 6, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 3, ForceMode.Impulse);
            }
            else
            {
                numJumps += 1;
                lizard.AddForce(Vector3.right * 4, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 2, ForceMode.Impulse);
            }

            //starts timer to wait before being able to jump again
            StartCoroutine(JumpTimer());
        }
    }

    /// <summary>
    /// Timer at the beginning of the minigame, waits 3 seconds before starting the game
    /// </summary>
    /// <returns></returns>
    public IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(GameTimer());
        intro.gameObject.SetActive(false);
    }

    /// <summary>
    /// Timer for game length, counts down from 30, updating UI and ending game once time is up
    /// </summary>
    /// <returns></returns>
    public IEnumerator GameTimer()
    {
        gameRunning = true;
        
        for(int i = 30; i >= 0; i--)
        {
            timer.text = "Time: " + i;
            yield return new WaitForSeconds(1);
        }

        //ends game and displays how many points were added to the leap score
        gameRunning = false;
        result.gameObject.SetActive(true);
        
        if(numJumps >= 65)
        {
            manager.playerLeap += 2;
            result.text = "+ " + 2 + " leap!";
        }
        else if( numJumps >= 35)
        {
            manager.playerLeap += 1;
            result.text = "+ " + 1 + " leap!";
        }
        else
        {
            result.text = "+ " + 0 + " leap";
        }

        //waits 5 seconds for player to process information before returning to hub
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Timer between jumps, waits 1 second then allows jumps and resets marker position
    /// </summary>
    /// <returns></returns>
    public IEnumerator JumpTimer()
    {
        justJumped = true;
        yield return new WaitForSeconds(1);
        justJumped = false;
        marker.transform.position = markStart.transform.position;
    }
}
