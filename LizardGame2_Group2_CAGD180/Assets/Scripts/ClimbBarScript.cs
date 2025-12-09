using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;

/*
 * Kafka Suenishi
 * calculates units climbed and moves lizard up
 * 12/02/25
 */

public class ClimbBarScript : MonoBehaviour
{


    public GameObject lizard;
    public int lizardSpeed = 5;
    public int unitsClimbed;

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
    private Vector3 barOGPosition;

    public TMP_Text climbed;


    // Start is called before the first frame update
    void Start()
    {
        //sets bar left/right boundaries and initial starting point
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
        barOGPosition = bar.transform.position;

       
    }

    void Update()
    {
        BarMovement();
        ColorHit();
        StartCoroutine(WaitToClimb());
        climbed.text = "units climbed: " + unitsClimbed;
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


    /// <summary>
    /// Calculates units moved depending on which color player released bar on 
    /// </summary>
    private void ColorHit()
    {

        Vector3 raycastOrigin = transform.position + Vector3.back * 0.75f;

        Debug.DrawRay(raycastOrigin, Vector3.forward, Color.green);

        if (Input.GetKeyUp(KeyCode.Space))
        {

            RaycastHit hit;

            if (Physics.Raycast(raycastOrigin, Vector3.forward, out hit, 1f))
            {
                if (hit.collider.gameObject.tag == "green")
                {
                    lizard.transform.position += Vector3.up * 100 * Time.deltaTime * lizardSpeed;
                    unitsClimbed += 20;
                    print("green");
                }
                else if (hit.collider.gameObject.tag == "yellow")
                {
                    lizard.transform.position += Vector3.up * 50 * Time.deltaTime * lizardSpeed;
                    unitsClimbed += 10;
                    print("yellow");
                }
                else if (hit.collider.gameObject.tag == "red")
                {
                    lizard.transform.position += Vector3.up * 25 * Time.deltaTime * lizardSpeed;
                    unitsClimbed += 5;
                    print("red");
                }
            }
        }
       //StartCoroutine(WaitToClimb());
    }




    void LizardWiggle()
    {
        //lizard.transform.Rotate(30 * direction, 0, 0);
        // farthest value: 16
        //closest value: -16
    }

   private IEnumerator WaitToClimb()
    {
      
        yield return new WaitForSeconds(2);
        bar.transform.position = barOGPosition;
    }

    

}
