using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

/*
 * Kafka Suenishi
 * 11/18/25
 * player presses button and releases at correct timing to make lizard climb
 */
public class ClimbTraining : MonoBehaviour
{

    //Press and hold button and release along bar
    //if release in red 1 point, yellow 2, green 3
    //depending on final number of points, increase climb (ie 9 points = 3 climb, 0-1=0)
    // 0-2 points per training
    //30 seconds max 

    private int score;
    private int climbScore;
    public GameObject lizard;

    public GameObject redZone;
    public GameObject yellowZone;
    public GameObject greenZone;

    public GameObject bar;
    public GameObject leftPoint;
    public GameObject rightPoint;
    private Vector3 leftPos;
    private Vector3 rightPos;
    public int speed;
    public bool goingLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BarMovement();
    }

    void BarMovement()
    {
        if (goingLeft)
        {
            if (transform.position.x <= leftPos.x)
            {
                goingLeft = false;
            }
            else
            {
                transform.position += Vector3.left * Time.deltaTime * speed;
            }
        }
        else
        {
            if (transform.position.x >= rightPos.x)
            {
                goingLeft = true;
            }
            else
            {
                transform.position += Vector3.right * Time.deltaTime * speed;
            }

        }
    }


    void LizardMovement()
    {
      
    }
}
