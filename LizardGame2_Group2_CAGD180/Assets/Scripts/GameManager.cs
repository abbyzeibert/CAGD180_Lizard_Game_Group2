using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abby Zeibert
 * 12/08/2025
 * Holds player information between scenes
 */

public class GameManager : MonoBehaviour
{
    //player stats
    public int playerSpeed;
    public int playerClimb;
    public int playerLeap;
    public float maxPlayerStamina = 1;

    //game state variables
    public int money = 0;
    public int trainingsDone = 0;
    public int currentRace = 0;


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
