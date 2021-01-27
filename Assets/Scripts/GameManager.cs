using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Config
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;
    [SerializeField] TMPro.TextMeshProUGUI livesText;
    [SerializeField] TMPro.TextMeshProUGUI scoreText;

    // To Make sure there is only One Game Manager present at a time
    private void Awake()
    {
        int GameManagerNumber = FindObjectsOfType<GameManager>().Length;
        if(GameManagerNumber > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    // To Count Score
   public void ProcessScore(int fireValue)

    {
        score = score + fireValue; // to calculate score on each pickup
        scoreText.text = score.ToString(); // to display score
    }    


    // To keep track of number of lives
  public void ProcessDeath()
    {
        if(playerLives > 0)
        {
            StartCoroutine(RemoveLife()); // to remove life and reload current scene
        }
        else
        {
            StartCoroutine(gameOver()); // to delay load the game over scene  
        }

    }

    // To load game over scene
    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(0.1f); // delay game over screen
        SceneManager.LoadScene(4); // load game over screen
    }

    IEnumerator RemoveLife()
    {
        playerLives--; // to reduce lives
        yield return new WaitForSeconds(0.1f); // delay level restart
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // get current scene index
        SceneManager.LoadScene(currentSceneIndex); // load current scene
        livesText.text = playerLives.ToString(); // to display lives  

    }
} 


   
