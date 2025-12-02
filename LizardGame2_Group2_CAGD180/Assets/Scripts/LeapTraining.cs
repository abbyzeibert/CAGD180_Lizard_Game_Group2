using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeapTraining : MonoBehaviour
{
    public bool gameRunning = false;
    public bool justJumped = false;

    public int leapToAdd = 0;
    public int numJumps = 0;
    public float markerSpeed = 5;

    private float markDistance;
    public float maxDistance = 15;
    public int direction = 1;
    public GameObject markStart;

    public Rigidbody lizard;
    public GameObject marker;
    public GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(WaitToStart());
    }

    // Update is called once per frame
    void Update()
    {
        if (gameRunning && !justJumped)
        {
            markDistance = Vector3.Distance(markStart.transform.position, marker.transform.position);

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


        if(gameRunning && Input.GetKeyDown(KeyCode.Space) && !justJumped)
        {
            if(markDistance > 12)
            {
                numJumps += 3;
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
            StartCoroutine(JumpTimer());
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

        if(numJumps >= 30)
        {
            manager.playerLeap += 2;
        }
        else if( numJumps >= 15)
        {
            manager.playerLeap += 1;
        }

        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(1);
    }

    public IEnumerator JumpTimer()
    {
        justJumped = true;
        yield return new WaitForSeconds(1);
        justJumped = false;
        marker.transform.position = markStart.transform.position;
    }
}
