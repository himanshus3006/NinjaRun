using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformVertical : MonoBehaviour
{
    // Config
    [SerializeField] float moveSpeed = 2.0f;

    // Cache Component References 
    Rigidbody2D myRigidBody2D;


    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }
    void Move()
    {
        myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x,moveSpeed); // move platform vertically

    }


    // To keep the platform within the box collider 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "MovingPlatform")
        {
            moveSpeed = -moveSpeed;
        }
    }
}
