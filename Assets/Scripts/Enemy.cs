using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 3.0f;

    // Cache Component References
    Rigidbody2D myRigidBody2D;
    Animator myAnimator2D;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myAnimator2D = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walk();
    }

    // To Move Enemy 
    private void Walk()
    {
        myRigidBody2D.velocity = new Vector2(moveSpeed, myRigidBody2D.velocity.y);
        if(myRigidBody2D.IsTouchingLayers(LayerMask.GetMask("Player"))) // returns true if enemy collides the player 
        {
            myRigidBody2D.velocity = new Vector2(0.0f, 0.0f);// stops the enemy movement 
            myAnimator2D.SetTrigger("Idle"); // changes animation to idle
        }
    }


    // To Change direction on Trigger Exit 
    private void OnTriggerExit2D(Collider2D collision)
    {

        moveSpeed = -moveSpeed; // to change direction
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed), 1.0f); // to flip character

    } 
}

