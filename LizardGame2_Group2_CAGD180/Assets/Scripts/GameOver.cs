using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*
 * Topher Overbey
 * 10/2/2025
 * Manage UI elements of the Game Over screen
*/


public class GameOver : MonoBehaviour
{
    public GameManager manager;

    public void Start()
    {
        manager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    /// <summary>
    /// Opens the Main Game Sceane when the play again button is pressed
    /// </summary>
    public void PlayAgainButton(int sceneIndex)
    {
        if(sceneIndex == 6 || sceneIndex == 7 || sceneIndex == 1)
        {
            SceneManager.LoadScene(sceneIndex);
        }
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
