using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    //Config
    public GameObject gameManager;
    
    // To load First Level
    public void StartGame ()
    {
        SceneManager.LoadScene(2);
    }

    // To load Tutorial
    public void Tutorial()
    {
        SceneManager.LoadScene(1);
    }

    // To load Main Menu
    public void MainMenu ()
    {
        SceneManager.LoadScene(0);
        // to find and destroy game manager to reset stats at the start of a new game
      gameManager = GameObject.Find("GameManager");
        Destroy(gameManager); 
    }

    // To Quit Game
    public void GameOver ()
    {
        Application.Quit();
    }
}
