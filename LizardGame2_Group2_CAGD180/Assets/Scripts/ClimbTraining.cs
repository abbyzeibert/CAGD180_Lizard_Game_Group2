using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
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

    public TMP_Text climbed;

    private bool gameStart = false;




    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
       
    }


    

/*
    private IEnumerable StartMiniGame()
    {

    }

  private IEnumerable GameLoop()
    {

    }

    */

    //press + hold space to start
    //after bar is released, wait 3 seconds while lizard moves
    //reset bar position 

}

