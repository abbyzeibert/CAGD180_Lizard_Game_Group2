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
    /// <summary>
    /// Opens the Main Game Sceane when the play again button is pressed
    /// </summary>
    public void PlayAgainButton(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
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
