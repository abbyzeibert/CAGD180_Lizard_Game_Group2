using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public GameObject[] placement;
    public int curPlace = 0;
    public bool player = false;

    public GameObject firstPod;
    public GameObject secondPod;
    public GameObject thirdPod;
    public GameObject offScreen;

    public GameManager manager;

    public void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter(Collider other)
    {
        placement[curPlace] = other.gameObject;

        if (other.GetComponent<Lizard>().isPlayer)
        {
            player = true;
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
        SceneManager.LoadScene(1);
    }
}
