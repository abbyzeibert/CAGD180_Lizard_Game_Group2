using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    public TMP_Text timer;
    public TMP_Text startText;
    public TMP_Text startText1;

    public GameManager manager;

    private bool gameStart = false;

    // Start is called before the first frame update
    void Start()
    {
        //sets bar left/right boundaries and initial starting point
        leftPos = leftPoint.transform.position;
        rightPos = rightPoint.transform.position;
        barOGPosition = greenZone.transform.position;

        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        StartCoroutine(GameTimer());
    }

    void Update()
    {
        BarMovement();
        ColorHit();
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
        
        barOGPosition = greenZone.transform.position;
    }


    /// <summary>
    /// Calculates units moved depending on which color player released bar on 
    /// </summary>
    private void ColorHit()
    {

        Vector3 raycastOrigin = transform.position + Vector3.back * 0.75f;

        //making sure raycast is positioned correctly
        //Debug.DrawRay(raycastOrigin, Vector3.forward, Color.green);

        if (Input.GetKeyUp(KeyCode.Space))
        {

            RaycastHit hit;

            //calculates how many units to climb depending on color raycast hits
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
                    lizard.transform.position += Vector3.down * 25 * Time.deltaTime * lizardSpeed;
                    unitsClimbed -= 5;
                    print("red");
                }
            }
        }
      
    }




    void LizardWiggle()
    {
        //lizard.transform.Rotate(30 * direction, 0, 0);
        // farthest value: 16
        //closest value: -16
    }

   
    

    private IEnumerator GameTimer()
    {
        if (Input.GetKeyDown (KeyCode.Space))
        {
            startText.enabled = false;
            startText1.enabled = false;
            gameStart = true;



            for (int i = 30; i >= 0; i--)
            {
                timer.text = "Time: " + i;
                yield return new WaitForSeconds(1);
            }
        }

        gameStart = false;
        
        
        
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(1);
    }
    

}
