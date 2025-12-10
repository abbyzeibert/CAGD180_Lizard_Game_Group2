using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lizard : MonoBehaviour
{
    public int runSpeed;
    public int climbSpeed;
    public int leapForce;
    public float maxStamina = 1.0f;
    public float stamina;

    public bool shouldRun = true;
    public bool shouldClimb = false;
    public bool shouldLeap = false;
    public bool isLeaping = false;

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
            maxStamina = manager.maxPlayerStamina;
        }
        else if (isGod) 
        {
            runSpeed = 9;
            climbSpeed = 9;
            leapForce = 9;
            maxStamina = 2f;
        }
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

        StartCoroutine(StartRace());
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
        if (isLeaping && other.CompareTag("Landing"))
        {
            Waypoint landingPoint = other.GetComponent<Waypoint>();
            SetNextPoint(track.levelWaypoints[landingPoint.nextPoint], landingPoint.waypointType);
        }
    }

    public void SetNextPoint(Waypoint nextPoint, Type thisType)
    {
        curPoint = nextPoint;

        StopCoroutine("RunWiggle");
        StopCoroutine("ClimbWiggle");

        shouldRun = shouldClimb = shouldLeap = isLeaping = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
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

    public void Run()
    {
        if (shouldRun)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), runSpeed * stamina * Time.deltaTime);
        }
    }

    public void Climb()
    {
        if (shouldClimb)
        {
            transform.position = Vector3.MoveTowards(transform.position, curPoint.GetPoint(), climbSpeed * stamina * Time.deltaTime);
        }
    }

    public void Leap()
    {
        if (shouldLeap)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;

            rb.AddForce(Vector3.right * leapForce * stamina, ForceMode.Impulse);
            rb.AddForce(Vector3.up * ((float)leapForce / 2) * stamina, ForceMode.Impulse);
            shouldLeap = false;
            isLeaping = true;
        }
    }

    public void DecreseStamina()
    {
        if(stamina > 0.5f)
        {
            stamina -= 0.05f;
        }
    }

    public IEnumerator RunWiggle()
    {
        int direction = 1;
        while (shouldRun)
        {
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
            yield return new WaitForSeconds(1 / (float)runSpeed);
        }
    }

    public IEnumerator ClimbWiggle()
    {
        int direction = 1;
        while (shouldClimb)
        {
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
            yield return new WaitForSeconds(1 / (float)climbSpeed);
        }
    }

    public IEnumerator StartRace()
    {
        shouldRun = false;
        yield return new WaitForSeconds(3);
        shouldRun = true;
        InvokeRepeating("DecreseStamina", 0, 1f);
    }

}
