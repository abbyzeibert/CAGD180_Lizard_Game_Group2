using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * Abby Zeibert
 * 12/10/2025
 * Displays player stats in hub scene
 */

public class HubStatDisplay : MonoBehaviour
{
    //updates with player variables
    public TMP_Text speed;
    public TMP_Text climb;
    public TMP_Text leap;
    public TMP_Text stamina;
    public TMP_Text money;
    public TMP_Text trainings;

    //buttons to send player to races
    public GameObject raceButton;
    public GameObject finalRaceButton;

    private GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        //initializes text values based on game manager's stored values
        //if a player has reached or exceeded the maximum value of a stat, 
        //sets stat to max and turns text yellow
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        speed.text = "Speed: " + manager.playerSpeed;
        if(manager.playerSpeed >= 10)
        {
            manager.playerSpeed = 10;
            speed.text = "Speed: " + manager.playerSpeed;
            speed.color = Color.yellow;
            speed.text += " (Max!)";
        }

        climb.text = "Climb: " + manager.playerClimb;
        if(manager.playerClimb >= 10)
        {
            manager.playerClimb = 10;
            climb.text = "Climb: " + manager.playerClimb;
            climb.color = Color.yellow;
            climb.text += " (Max!)";
        }

        leap.text = "Leap: " + manager.playerLeap;
        if(manager.playerLeap >= 10)
        {
            manager.playerLeap = 10;
            leap.text = "Leap: " + manager.playerLeap;
            leap.color = Color.yellow;
            leap.text += " (Max!)";
        }

        //multiplies stamina value by 100 for easier understanding
        stamina.text = "Stamina: " + (Mathf.Round(manager.maxPlayerStamina * 100));
        money.text = "Money: " + manager.money;
        trainings.text = "Trainings Done: " + manager.trainingsDone + " / 4";

        //Hides or shows race buttons based on current race to send to correct scene
        if(manager.currentRace == 0 || manager.currentRace == 1)
        {
            raceButton.SetActive(true);
            finalRaceButton.SetActive(false);
        }
        else
        {
            raceButton.SetActive(false);
            finalRaceButton.SetActive(true);
        }
    }

}
