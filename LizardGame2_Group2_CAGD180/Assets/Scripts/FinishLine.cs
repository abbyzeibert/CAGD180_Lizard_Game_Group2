using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Timeline;

public class FinishLine : MonoBehaviour
{
    public GameObject[] placement;
    public int curPlace = 0;
    public bool player = false;
    public bool raceActive = false;
    public bool justCheered = false;
    public bool wonRace = false;

    public GameObject firstPod;
    public GameObject secondPod;
    public GameObject thirdPod;
    public GameObject offScreen;

    public GameObject markStart;
    public GameObject marker;
    private float markDistance;
    public float maxDistance = 10;
    public int direction;
    public float markerSpeed;
    private Lizard playerScript;

    public TMP_Text startText;

    public GameManager manager;

    public void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerScript = GameObject.Find("Player Lizard").GetComponent<Lizard>();
        StartCoroutine(StartRace());
    }

    public void Update()
    {
        if(raceActive && !justCheered)
        {
            markDistance = Vector3.Distance(markStart.transform.position, marker.transform.position);

            if (markDistance >= maxDistance)
            {
                direction = -1;
            }
            else if (markDistance <= 0.1)
            {
                direction = 1;
            }

            marker.transform.position += Vector3.up * markerSpeed * direction * Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if(markDistance >= 7.5f)
                {
                    playerScript.stamina += 0.15f;
                }

                StartCoroutine(CheerTimer());
            }
        }


    }

    public void OnTriggerEnter(Collider other)
    {
        placement[curPlace] = other.gameObject;

        if (other.GetComponent<Lizard>().isPlayer)
        {
            player = true;
            raceActive = false;
        }
        else
        {
            player = false;
        }

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

        other.GetComponent<Lizard>().enabled = false;

        curPlace++;
    }


    public void SendToHub()
    {
        if (manager.currentRace == 0 || manager.currentRace == 1)
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
            SceneManager.LoadScene(1);
        }
    }

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

    public IEnumerator CheerTimer()
    {
        justCheered = true;
        yield return new WaitForSeconds(Random.Range(2.0f,5.0f));
        justCheered = false;
        marker.transform.position = markStart.transform.position;
    }
}
