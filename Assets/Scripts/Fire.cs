using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    //Config
    [SerializeField] int fireValue = 10;
    public AudioClip pickUp = null;

    private void OnTriggerEnter2D(Collider2D collision) // check for collision 
    {
        FindObjectOfType<GameManager>().ProcessScore(fireValue); // adds to score whenever fire is destroyed
        AudioSource.PlayClipAtPoint(pickUp,Camera.main.transform.position); // plays audio relative to world space from the main camera position 
        Destroy(gameObject); // destroy the fire
    }
}

    
 
