using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public int runSpeed;
    public int climbSpeed;
    public int leapForce;

    public bool isPlayer;

    public GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        if (isPlayer)
        {
            runSpeed = manager.playerSpeed;
            climbSpeed = manager.playerClimb;
            leapForce = manager.playerLeap;
        }
        else
        {
            switch (manager.currentRace)
            {
                case 0:
                    runSpeed = (int)Random.Range(1, 4);
                    climbSpeed = (int)Random.Range(1, 4);
                    leapForce = (int)Random.Range(1, 4);
                    break;
                case 1:
                case 2:
                    runSpeed = (int)Random.Range(4, 7);
                    climbSpeed = (int)Random.Range(4, 7);
                    leapForce = (int)Random.Range(4, 7);
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
