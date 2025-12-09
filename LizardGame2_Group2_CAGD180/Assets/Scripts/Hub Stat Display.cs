using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HubStatDisplay : MonoBehaviour
{
    public TMP_Text speed;
    public TMP_Text climb;
    public TMP_Text leap;
    public TMP_Text stamina;
    public TMP_Text money;
    public TMP_Text trainings;

    public GameObject raceButton;
    public GameObject finalRaceButton;

    private GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        speed.text = "Speed: " + manager.playerSpeed;
        climb.text = "Climb: " + manager.playerClimb;
        leap.text = "Leap: " + manager.playerLeap;
        stamina.text = "Stamina: " + (Mathf.Round(manager.maxPlayerStamina * 100));
        money.text = "Money: " + manager.money;
        trainings.text = "Trainings Done: " + manager.trainingsDone + " / 7";

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
