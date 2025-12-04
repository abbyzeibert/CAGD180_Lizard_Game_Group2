using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HubStatDisplay : MonoBehaviour
{
    public TMP_Text speed;
    public TMP_Text climb;
    public TMP_Text leap;

    private GameManager manager;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        speed.text = "Speed: " + manager.playerSpeed;
        climb.text = "Climb: " + manager.playerClimb;
        leap.text = "Leap: " + manager.playerLeap;
    }

}
