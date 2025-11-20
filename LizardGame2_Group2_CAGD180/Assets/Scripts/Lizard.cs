using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public int runSpeed;
    public int climbSpeed;
    public int leapForce;

    public bool shouldRun = true;
    public bool shouldClimb = false;
    public bool shouldLeap = false;

    public bool isPlayer;
    public bool isGod;

    public Rigidbody rb;
    public GameManager manager;
    public WaypointManager track;
    public Waypoint curPoint;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        track = GameObject.Find("Waypoint Manager").GetComponent<WaypointManager>();

        curPoint = track.levelWaypoints[0];

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

        if(Vector3.Distance(transform.position, curPoint.toMove) < 0.01)
        {
            Waypoint tempPoint = track.levelWaypoints[curPoint.nextPoint];
            SetNextPoint(tempPoint, curPoint.waypointType);
        }

        Run();
        Climb();
        Leap();
    }

    public void OnTriggerEnter(Collider other)
    {

    }

    public void SetNextPoint(Waypoint nextPoint, Type thisType)
    {
        curPoint = nextPoint;

        shouldRun = shouldClimb = shouldLeap = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        switch (thisType)
        {
            case Type.Run:
                shouldRun = true;
                transform.rotation = Quaternion.Euler(0, 0, 0);
                break;
            case Type.Climb:
                transform.rotation = Quaternion.Euler(0, 0, 90);
                shouldClimb = true;
                break;
            case Type.Leap:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                shouldLeap = true;
                break;
        }
    }

    public void Run()
    {
        if (shouldRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), runSpeed * Time.deltaTime);
        }
    }

    public void Climb()
    {
        if (shouldClimb)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), climbSpeed * Time.deltaTime);
        }
    }

    public void Leap()
    {
        if (shouldLeap)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

            rb.AddForce(Vector3.right * leapForce, ForceMode.Impulse);
            rb.AddForce(Vector3.up * ((float)leapForce / 2), ForceMode.Impulse);
            shouldLeap = false;
        }
    }
}
