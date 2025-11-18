using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public int runSpeed;
    public int climbSpeed;
    public int leapForce;

    public bool isPlayer;
    public bool isGod;

    public GameManager manager;
    public WaypointManager track;
    public Waypoint curPoint;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        track = GameObject.Find("Waypoint Manager").GetComponent<WaypointManager>();

        curPoint = GameObject.Find("Waypoint").GetComponent<Waypoint>();

        if (isPlayer)
        {
            runSpeed = manager.playerSpeed;
            climbSpeed = manager.playerClimb;
            leapForce = manager.playerLeap;
        }
        else if (isGod) 
        {
            runSpeed = 9;
            climbSpeed = 9;
            leapForce = 9;
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
        Run();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void Run()
    {
        transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), runSpeed * Time.deltaTime);
    }

    public void Climb()
    {

    }

    public void Leap()
    {

    }
}
