using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abby Zeibert
 * 12/10/2025
 * Controls lizard behavior in races
 */

public class Lizard : MonoBehaviour
{
    //lizard stats
    public int runSpeed;
    public int climbSpeed;
    public int leapForce;
    public float maxStamina = 1.0f;
    public float stamina;

    //lizard state variables
    public bool shouldRun = true;
    public bool shouldClimb = false;
    public bool shouldLeap = false;
    public bool isLeaping = false;

    //special lizard options
    public bool isPlayer;
    public bool isGod;

    //game objects
    public Rigidbody rb;
    public GameManager manager;
    public WaypointManager track;
    public Waypoint curPoint;


    // Start is called before the first frame update
    void Start()
    {
        //initilizes variables, finds game manager, track, and first waypoint
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        track = GameObject.Find("Waypoint Manager").GetComponent<WaypointManager>();

        curPoint = track.levelWaypoints[0];

        //sets this lizard's stats:

        //special case: player stats are pulled from game manager
        if (isPlayer)
        {
            runSpeed = manager.playerSpeed;
            climbSpeed = manager.playerClimb;
            leapForce = manager.playerLeap;
            maxStamina = manager.maxPlayerStamina;
        }
        //special case: god lizard stats are pre-determined to be difficult
        else if (isGod) 
        {
            runSpeed = 9;
            climbSpeed = 9;
            leapForce = 9;
            maxStamina = 2f;
        }
        //regular lizards: stats are randomly assigned based on current race
        else
        {
            switch (manager.currentRace)
            {
                case 0:
                    runSpeed = (int)Random.Range(1, 4);
                    climbSpeed = (int)Random.Range(1, 4);
                    leapForce = (int)Random.Range(1, 4);
                    maxStamina = Random.Range(1.0f, 1.5f);
                    break;
                case 1:
                case 2:
                    runSpeed = (int)Random.Range(4, 8);
                    climbSpeed = (int)Random.Range(4, 8);
                    leapForce = (int)Random.Range(4, 8);
                    maxStamina = Random.Range(1.25f, 2f);
                    break;
            }
        }
        stamina = maxStamina;

        //starts race countdown
        StartCoroutine(StartRace());
    }

    // Update is called once per frame
    void Update()
    {
        //when lizard makes it to the current waypoint, gets the next one
        if(Vector3.Distance(transform.position, curPoint.toMove) < 0.01)
        {
            Waypoint tempPoint = track.levelWaypoints[curPoint.nextPoint];
            SetNextPoint(tempPoint, curPoint.waypointType);
        }

        //lizard movement types, activated only when the given state is true
        Run();
        Climb();
        Leap();
    }

    public void OnTriggerEnter(Collider other)
    {
        //handles landing from a leap, pulls next waypoint from point stored landing zone
        if (isLeaping && other.CompareTag("Landing"))
        {
            Waypoint landingPoint = other.GetComponent<Waypoint>();
            SetNextPoint(track.levelWaypoints[landingPoint.nextPoint], landingPoint.waypointType);
        }
    }

    /// <summary>
    /// Updates which waypoint the lizard is heading to
    /// </summary>
    /// <param name="nextPoint"> the waypoint the lizard will go to </param>
    /// <param name="thisType"> the movement the lizard will switch to</param>
    public void SetNextPoint(Waypoint nextPoint, Type thisType)
    {
        //sets waypoint the lizard is going to
        curPoint = nextPoint;

        //stops movement coroutines from previous section
        StopCoroutine("RunWiggle");
        StopCoroutine("ClimbWiggle");

        //sets all movement states to false and activates all rigidbody constraints to stop falls
        shouldRun = shouldClimb = shouldLeap = isLeaping = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

        //activates next movement state, sets proper rotation, and begins movement coroutines
        switch (thisType)
        {
            case Type.Run:
                shouldRun = true;
                transform.rotation = Quaternion.Euler(0, 0, -15);
                StartCoroutine(RunWiggle());
                break;
            case Type.Climb:
                transform.rotation = Quaternion.Euler(0, 0, 75);
                shouldClimb = true;
                StartCoroutine(ClimbWiggle());
                break;
            case Type.Leap:
                transform.rotation = Quaternion.Euler(0, 0, 0);
                shouldLeap = true;
                break;
        }
    }

    /// <summary>
    /// Performs running movement state
    /// </summary>
    public void Run()
    {
        //when active, moves lizard towards waypoint based on run speed and current stamina
        if (shouldRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), runSpeed * stamina * Time.deltaTime);
        }
    }

    /// <summary>
    /// Performs climbing movement state
    /// </summary>
    public void Climb()
    {
        //when active, moves lizard towards waypoint based on climb speed and current stamina
        if (shouldClimb)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), climbSpeed * stamina * Time.deltaTime);
        }
    }

    /// <summary>
    /// Performs leaping movement state
    /// </summary>
    public void Leap()
    {
        if (shouldLeap)
        {
            //unfreezes x and y rigidboxy constraints
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

            //adds force at x1 strength forward and x0.5 strength up based on leap stat and current stamina
            rb.AddForce(Vector3.right * leapForce * stamina, ForceMode.Impulse);
            rb.AddForce(Vector3.up * ((float)leapForce / 2) * stamina, ForceMode.Impulse);

            //sets state to currently leaping
            shouldLeap = false;
            isLeaping = true;
        }
    }

    /// <summary>
    /// Performs stamina decreases if lizard has more than 0.5 left
    /// </summary>
    public void DecreseStamina()
    {
        if(stamina > 0.5f)
        {
            stamina -= 0.05f;
        }
    }

    /// <summary>
    /// Performs wiggle animation when lizard is in running state
    /// </summary>
    /// <returns></returns>
    public IEnumerator RunWiggle()
    {
        int direction = 1;
        while (shouldRun)
        {
            //rotates lizard, changes direction, then waits based on run speed to rotate again
            //higher run speed causes faster rotations
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
            yield return new WaitForSeconds(1 / (float)runSpeed);
        }
    }

    /// <summary>
    /// Performs wiggle animation when lizard is in climbing state
    /// </summary>
    /// <returns></returns>
    public IEnumerator ClimbWiggle()
    {
        int direction = 1;
        while (shouldClimb)
        {
            //rotates lizard, changes direction, then waits based on climb speed to rotate again
            //higher climb speed causes faster rotations
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
            yield return new WaitForSeconds(1 / (float)climbSpeed);
        }
    }

    /// <summary>
    /// Timer to wait before activating lizard run state, syncs with StartRace() in FinishLine
    /// </summary>
    /// <returns></returns>
    public IEnumerator StartRace()
    {
        //waits 3 seconds before activating movement and starts stamina decreases each second
        shouldRun = false;
        yield return new WaitForSeconds(3);
        shouldRun = true;
        InvokeRepeating("DecreseStamina", 0, 1f);
    }

}
