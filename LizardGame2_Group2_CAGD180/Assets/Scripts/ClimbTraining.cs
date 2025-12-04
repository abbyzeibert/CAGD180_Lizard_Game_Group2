using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
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

    public GameObject lizard;
    public int direction = 1;
    public int lizardSpeed = 5;

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
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;


       
    }

    // Update is called once per frame
    void Update()
    {
        BarMovement();
       
    }


    /// <summary>
    /// Moves black bar back and forth when player presses down spacebar and stops when released
    /// </summary>
    void BarMovement()
    {

        if (Input.GetKey(KeyCode.Space))
        {
            if (goingLeft)
            {
                if (bar.transform.position.x <= leftPos.x)
                {
                    goingLeft = false;
                }
                else
                {
                    bar.transform.position += Vector3.left * Time.deltaTime * speed;
                }
            }
            else
            {
                if (bar.transform.position.x >= rightPos.x)
                {
                    goingLeft = true;
                }
                else
                {
                    bar.transform.position += Vector3.right * Time.deltaTime * speed;
                }

            }
        }           
    }

  




   


    /* Coroutine: pause for 1-2 seconds while lizard climbs before player can press space again
     * 
     */

}

