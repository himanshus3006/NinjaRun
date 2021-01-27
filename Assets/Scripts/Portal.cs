using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    // When play enters load the next level
    private void OnTriggerEnter2D(Collider2D collision)
    {

        StartCoroutine (LoadNextLevel());

    }

    
    IEnumerator LoadNextLevel ()
    {
        yield return new WaitForSeconds(0.1f); // delay the load of next level
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // get current scene index
        SceneManager.LoadScene(currentSceneIndex + 1); // add to current scene index to load next scene index
    }
}
