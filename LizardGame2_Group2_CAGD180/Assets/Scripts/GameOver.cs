using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Topher Overbey
 * 10/2/2025
 * Manage UI elements of the Game Over screen
 * 
 * Abby Zeibert
 * 12/09/2025
 * Handles sending player to trainings
 */

public class GameOver : MonoBehaviour
{
    public GameManager manager;

    public void Start()
    {
        //finds game manager in scene
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Opens the Main Game Sceane when the play again button is pressed
    /// </summary>
    public void PlayAgainButton(int sceneIndex)
    {
        //if sending to races or hub, doesn't check how many trainings have been done
        if(sceneIndex == 6 || sceneIndex == 7 || sceneIndex == 1)
        {
            SceneManager.LoadScene(sceneIndex);
        }
        //only sends to a training if under max trainings and increments trainings done
        else if(manager.trainingsDone < 4)
        {
            manager.trainingsDone++;
            //loads the scene that the index is refenrceing
            SceneManager.LoadScene(sceneIndex);
        }

    }
    /// <summary>
    /// Quit the Game when the quit button is pressed
    /// </summary>
    public void quitGameButton()
    {
        //quits a build of the game, not used in the editor
        Application.Quit();
        print("QUIT THE GAME");
    }

}
