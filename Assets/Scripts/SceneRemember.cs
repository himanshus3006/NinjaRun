using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneRemember : MonoBehaviour
{
    //Config
    static SceneRemember instance = null;
    int startingSceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        if(!instance)
        {
            instance = this; // if there is no remembering pick ups left make this the remembering pick up
            SceneManager.sceneLoaded += OnSceneLoaded; // adds to list of delegates 
            startingSceneIndex = SceneManager.GetActiveScene().buildIndex; // get scene index
            DontDestroyOnLoad(gameObject); // dont destroy game object since it is the only game object
        }
        else if(instance != this)
        {
            Destroy(gameObject); // destroys remembering since more than one exists 
        }
    }

// to reset values before next scene start() method is called
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (startingSceneIndex !=  SceneManager.GetActiveScene().buildIndex) // if starting scene index is not the same as the current scene reset instance and destroy object before the start() of the next scene 

        { 
            instance = null; // reset instance to null
            SceneManager.sceneLoaded -= OnSceneLoaded; // removes from list of delegates
            Destroy(gameObject); // destroys previous rememberings 
        }
    }
}