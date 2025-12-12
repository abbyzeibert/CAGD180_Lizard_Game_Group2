using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Timeline;

/*
 * Abby Zeibert
 * 12/10/2025
 * Manages starting race, end placements, and cheering
 */

public class FinishLine : MonoBehaviour
{
    //Race end variables
    public GameObject[] placement;
    public int curPlace = 0;
    public bool player = false;
    public bool wonRace = false;

    //Podium positions
    public GameObject firstPod;
    public GameObject secondPod;
    public GameObject thirdPod;
    public GameObject offScreen;

    //Cheering & bar movement variables
    public GameObject markStart;
    public GameObject marker;
    private float markDistance;
    public float maxDistance = 10;
    public int direction;
    public float markerSpeed;
    public bool raceActive = false;
    public bool justCheered = false;
    private Lizard playerScript;

    //Intro countdown text and game manager
    public TMP_Text startText;

    public GameManager manager;

    /// <summary>
    /// Starts scene needs, finding game manager and player, and starting race countdown
    /// </summary>
    public void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerScript = GameObject.Find("Player Lizard").GetComponent<Lizard>();
        StartCoroutine(StartRace());
    }


    public void Update()
    {
        //Only moves marker and detects key presses if race is active and player has not just cheered
        if(raceActive && !justCheered)
        {
            //calculates distance marker is away from its start point
            markDistance = Vector3.Distance(markStart.transform.position, marker.transform.position);

            //turns marker around when it reaches the ends of its box
            if (markDistance >= maxDistance)
            {
                direction = -1;
            }
            else if (markDistance <= 0.1)
            {
                direction = 1;
            }

            //moves marker each frame
            marker.transform.position += Vector3.up * markerSpeed * direction * Time.deltaTime;

            //when space is pressed, adds stamina to player if marker was in green zone 
            //and starts a timer for when the player can cheer again
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(markDistance >= 7.5f)
                {
                    playerScript.stamina += 0.20f;
                }

                StartCoroutine(CheerTimer());
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //keeps track of the order lizards finished the race
        placement[curPlace] = other.gameObject;

        //checks if the current lizard is the player
        if (other.GetComponent<Lizard>().isPlayer)
        {
            player = true;
            raceActive = false;
        }
        else
        {
            player = false;
        }

        //moves lizards to correct podium spot based on placement
        //and updates player's current race and money when they finish
        //does not move to the next race if the player failed to make the podium
        if(curPlace == 0)
        {
            other.transform.position = firstPod.transform.position;
            if (player)
            {
                manager.money += 200;
                manager.currentRace++;
                wonRace = true;
            }
        }
        else if(curPlace == 1)
        {
            other.transform.position = secondPod.transform.position;
            if (player)
            {
                manager.money += 150;
                manager.currentRace++;
            }
        }
        else if (curPlace == 2)
        {
            other.transform.position = thirdPod.transform.position;
            if (player)
            {
                manager.money += 100;
                manager.currentRace++;
            }
        }
        else
        {
            other.transform.position = offScreen.transform.position;
            if (player)
            {
                manager.money += 50;
            }
        }

        //turns off lizard script when lizards finish so they stop moving
        other.GetComponent<Lizard>().enabled = false;
        other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        curPlace++;
    }

    /// <summary>
    /// If it is not the final race or the player did not win the final race, sends the player back to 
    /// the hub and resets the amount of trainings they have done.  Else, sends player to win screen
    /// </summary>
    public void SendToHub()
    {
        if (manager.currentRace == 0 || manager.currentRace == 1 || manager.currentRace == 2)
        {
            manager.trainingsDone = 0;
            SceneManager.LoadScene(1);
        }
        else if( wonRace)
        {
            SceneManager.LoadScene(8);
        }
        else
        {
            manager.trainingsDone = 3;
            manager.currentRace--;
            SceneManager.LoadScene(1);
        }
    }

    /// <summary>
    /// Countdown to start the race, updates text at start and activates race when done
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRace()
    {
        for (int i = 3; i >= 0; i--)
        {
            if( i > 0)
            {
                startText.text = i.ToString();
            }
            else
            {
                startText.text = "GO!";
            }
            yield return new WaitForSeconds(1);
        }
        raceActive = true;
    }

    /// <summary>
    /// Cooldown between cheer uses, waits a random amount of time between 2 and 5 seconds
    /// </summary>
    /// <returns></returns>
    public IEnumerator CheerTimer()
    {
        justCheered = true;
        yield return new WaitForSeconds(Random.Range(2.0f,5.0f));
        justCheered = false;
        marker.transform.position = markStart.transform.position;
    }
}
