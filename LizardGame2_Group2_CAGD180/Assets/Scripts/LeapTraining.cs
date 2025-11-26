using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeapTraining : MonoBehaviour
{
    public bool gameRunning = false;

    public int leapToAdd = 0;
    public int numJumps = 0;
    public float markerSpeed = 5;

    private float markDistance;
    public float maxDistance = 10;
    public int direction = 1;
    private Vector3 markStart;

    public Rigidbody lizard;
    public GameObject marker;

    // Start is called before the first frame update
    void Start()
    {
        markStart = marker.transform.position;
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update()
    {
        while (gameRunning)
        {
            markDistance = Vector3.Distance(markStart, marker.transform.position);

            if(markDistance >= maxDistance)
            {
                direction = -1;
            }
            else if(markDistance <= 0.01)
            {
                direction = 1;
            }

            marker.transform.position += Vector3.right * markerSpeed * direction * Time.deltaTime;
        }


        if(gameRunning && Input.GetKeyDown(KeyCode.Space))
        {
            if(markDistance > 9)
            {
                numJumps += 3;
                lizard.AddForce(Vector3.right * 5, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 2.5f, ForceMode.Impulse);
            }
            else if(markDistance > 6)
            {
                numJumps += 2;
                lizard.AddForce(Vector3.right * 3.5f, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 1.75f, ForceMode.Impulse);
            }
            else
            {
                numJumps += 1;
                lizard.AddForce(Vector3.right * 2, ForceMode.Impulse);
                lizard.AddForce(Vector3.up * 1, ForceMode.Impulse);
            }
        }
    }

    public IEnumerator WaitToStart()
    {
        yield return new WaitForSeconds(3);
        StartCoroutine(GameTimer());
    }

    public IEnumerator GameTimer()
    {
        gameRunning = true;
        yield return new WaitForSeconds(30);
        gameRunning = false;
    }
}
