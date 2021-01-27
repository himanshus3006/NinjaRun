using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    //Config
    [SerializeField] float moveSpeed = 5.0f;
    [SerializeField] float jumpSpeed = 5.0f;
    [SerializeField] float climbSpeed = 5.0f;
    [SerializeField ]float groundDistance = 0.55f;
    [SerializeField] AudioClip death = null;
    
    private float scaleX = 1.0f;
    private float scaleY = 1.0f;
    float gravityScaleDefault;


    //State
    private bool isAlive = true;

    // Cache Component References
    Rigidbody2D myRigidBody;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider2D;



    // Start is called before the first frame update
    void Start()
    {
        // Get Components
        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider2D = GetComponent<CapsuleCollider2D>();
        gravityScaleDefault = myRigidBody.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAlive) { return; } // to contraint movement of player if dead 
        Run();
        Jump();
        ClimbLadder();
        FlipPlayer();
        Die();
    }





    // To move player horizontally 
    private void Run()
    {
        float moveControl = CrossPlatformInputManager.GetAxis("Horizontal"); // gets input from user
        Vector2 playerVelocity = new Vector2(moveControl * moveSpeed, myRigidBody.velocity.y); // defines velocity by which player moves
        myRigidBody.velocity = playerVelocity; // assigns velocity to the player

        // To change animation between idle and running according to input
        if (moveControl > 0 || moveControl < 0)
        {
            myAnimator.SetBool("Running", true);
        }
        else if (moveControl == 0)
        {
            myAnimator.SetBool("Running", false);
        }

    }




    // To make character jump 
    private void Jump()
    {
        // To check collision with ground
        bool CheckGround()
        {
            RaycastHit2D rayHit = Physics2D.Raycast(myRigidBody.transform.position, Vector2.down, groundDistance, LayerMask.GetMask("Platform")); // creates a ray from the object position as origin and sends it downwards
            if (rayHit.collider != null) // if ray hits then true , else false 
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        if (CheckGround())
        {
            bool isColliding = !myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Platform")); // returns true if player is not colliding with layer ( need to manually assign layer )


            if (CrossPlatformInputManager.GetButtonDown("Jump")) // returns true if spacebar is pressed 

            {
                if (!isColliding) // check if collision occurs to stop jump 
                {
                    Vector2 jumpVelocity = new Vector2(0f, jumpSpeed); // define jump velocity
                    myRigidBody.velocity = myRigidBody.velocity + jumpVelocity; // assign jump velocity to player
                    isColliding = true;  // since collision does not occur 
                }
            }
            myAnimator.SetBool("Jumping", isColliding); // assign jump animation based on collision  
        }
        
    }



    // To climb ladder
    private void ClimbLadder()
    {
        bool isColliding = myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladders")); // returns true if player is colliding with layer ( need to manually assign layer )

        float moveControl = CrossPlatformInputManager.GetAxis("Vertical"); // gets input from user

        if (isColliding)
        {
            
            Vector2 playerVelocity = new Vector2(myRigidBody.velocity.x, moveControl * climbSpeed); // defines velocity by which player moves
            myRigidBody.velocity = playerVelocity; // assigns velocity to the player
            myAnimator.SetBool("Jumping", false); // removes jump animation while climbing 
           isColliding = true; // since collision occurs
            myRigidBody.gravityScale = 0.0f; // remove gravity while climbing
        }
        else { myRigidBody.gravityScale = gravityScaleDefault; } // set gravity back to orginal 
        myAnimator.SetBool("Climbing", isColliding); // To change animation to climbing according to collision
    }

   



    // To change direction of facing 
    private void FlipPlayer()
    {
        float moveControl = CrossPlatformInputManager.GetAxis("Horizontal"); // gets input from user
        if(moveControl > 0 ) // if input is right 
        {
            transform.localScale = new Vector2(scaleX, scaleY); // character faces right 
        }
        else if (moveControl<0) // if input is left
        {
            transform.localScale = new Vector2(-scaleX, scaleY); // character faces left
        }
    }


    // To kill player
    private void Die()
    {
        if (myBodyCollider2D.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards"))) // returns true if player collides with enemy 
        {
            isAlive = false; // to stop input from occuring when dead
            myAnimator.SetTrigger("Dying"); // changes animation to dying
            FindObjectOfType<GameManager>().ProcessDeath(); // return lives count to game manager
            GetComponent<AudioSource>().clip = death; // get death audio clip
            GetComponent<AudioSource>().Play(); // play death audio clip
        }


    }

}
