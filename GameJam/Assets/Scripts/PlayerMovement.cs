using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float gravityScale = 1f;
    [SerializeField]
    private float wallClingGravity = 0.5f;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float jumpTimer = 0.5f;
    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private int wallContact;

    [SerializeField]
    private bool jumping = false;
    private bool pressedJump = false;
    private bool releasedJump = false;
    private Rigidbody2D rigidBody;
    private bool startTimer = false;
    private float timer;
    private float moveVelocity;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        timer = jumpTimer;
    }

    void Update() {
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) {
            pressedJump = true;
        }
        if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
            releasedJump = true;
        }

        if(startTimer) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                releasedJump = true;
            }
        }

        moveVelocity = 0;
        //Left Right Movement
        if(wallContact != -1) {
            if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
            {
                moveVelocity = -speed;
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
            }
        }
        if(wallContact != 1) {
            if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
            {
                moveVelocity = speed;
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
            }
        }
        if(!jumping) {
            if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.A)) {
                moveVelocity = 0f;
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
            }
            if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D)) 
            {
                moveVelocity = 0f;
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
            }
        }
        
        
    }

    void FixedUpdate() {
        if(pressedJump) {
            if(grounded) {
                StartFloorJump();
            }
            if(wallContact == -1) {
                StartWallJump(1);
            }
            else if(wallContact == 1) {
                StartWallJump(-1);
            }
            
        }
        if(releasedJump) {
            StopJump();
        }
        if(!jumping) {
            if(wallContact == 0) {                
                rigidBody.gravityScale = gravityScale;
            }
            else {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                rigidBody.gravityScale = wallClingGravity;
            }
        }
        
    }

    void StartFloorJump() {
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        pressedJump = false;
        jumping = true;
        startTimer = true;
    }

    void StartWallJump(int direction) {
        rigidBody.gravityScale = 0f;
        rigidBody.velocity = new Vector2(0, 0);
        rigidBody.AddForce(new Vector2(direction * jumpForce, jumpForce), ForceMode2D.Impulse);
        pressedJump = false;
        jumping = true;
        startTimer = true;
    }

    void StopJump() {
        rigidBody.gravityScale = gravityScale;
        releasedJump = false;
        jumping = false;
        timer = jumpTimer;
        startTimer = false;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor") {
            grounded = true;
        }
        else if(other.gameObject.tag == "Wall") {
            rigidBody.velocity = new Vector2(0, 0);
            if(other.gameObject.transform.position.x < this.transform.position.x) {
                wallContact = -1;
            }
            else if(other.gameObject.transform.position.x > this.transform.position.x){
                wallContact = 1;
            }
            else if(other.gameObject.transform.position.y < this.transform.position.y) {
                grounded = true;
            }
        }
        
    }
    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Floor") {
            grounded = false;
        }
        else if(other.gameObject.tag == "Wall") {
            wallContact = 0;
        }
    }

}
