using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
/*
 * Topher Overbey
 * 11/20/25
 * Controls the speed training scene's lizard and gives off the number to add onto the normal speed
*/

public class SpeedTraining : MonoBehaviour
{
    public int clickAmount = 0;
    public int speedToAdd = 0;
    public bool isTrainingMoveing = false;
    public GameManager manager;
    public GameObject instructions;
    public TMP_Text scoreText;

    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        //get the game manager script
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        //start the waiting time and set the instructions for the training to invisabl
        StartCoroutine(WaitToStart());
        instructions.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        SpaceButtonClickCount();
        scoreText.text = "Click Amount: " + clickAmount.ToString();
    }
    public void SpaceButtonClickCount()
    {
        if (Input.GetKeyUp(KeyCode.Space) && isTrainingMoveing)
        {//when space is pressed add click amount to iself plus 1
            clickAmount++;
            transform.Rotate(0, 0, (30 * direction));
            direction *= -1;
        }//if click amount is over 270 add 3 speed
        if (clickAmount > 270)
        {
            speedToAdd = 3;
        }
        //if click amount is over 140 add 2 speed
        if (clickAmount > 140)
        {
            speedToAdd = 2;
        }//if click amount is less than 140 and more than 90 add 1 speed
        else if (clickAmount > 90)
        {
            speedToAdd = 1;
        }//if click amount is less than 90 add 0 speed
        else
        {
            speedToAdd = 0;
        }
    }
    public IEnumerator WaitToStart()
    {

        yield return new WaitForSeconds(3);
        StartCoroutine(StartTraining());
    }
    public IEnumerator StartTraining()
    {
        //set bool for traing to true, and set instructions to active then wait 30 sec
        isTrainingMoveing = true;
        instructions.SetActive(true);
        yield return new WaitForSeconds(30);
        manager.playerSpeed += speedToAdd;
        //set instructions and bool for training to false
        instructions.SetActive(false);
        isTrainingMoveing = false;
        //wait for 7 seconds then head back to main scene
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(1);
    }
}
