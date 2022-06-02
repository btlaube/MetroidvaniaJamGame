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
    //[SerializeField]
    //private bool grounded = false;
    //[SerializeField]
    //private int wallContact;

    [SerializeField]
    private bool jumping = false;
    private bool pressedJump = false;
    private bool releasedJump = false;
    private Rigidbody2D rigidBody;
    private bool startTimer = false;
    private float timer;
    private float moveVelocity;

    //public bool isJumping;
    //public float jumpSpeed = 8f;
    private float rayCastLengthCheck = 0.1f;
    private float width;
    private float height;

    void Awake() {
        rigidBody = GetComponent<Rigidbody2D>();
        timer = jumpTimer;
        width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
    }

    void Update() {

        /*
        RaycastHit2D up = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.up), 1f, 6);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.up) * 1f, Color.red);
        RaycastHit2D down = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.up), 1f, 6);
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.up) * 1f, Color.red);
        RaycastHit2D right = Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.right), 1f, 6);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector2.right) * 1f, Color.red);
        RaycastHit2D left = Physics2D.Raycast(transform.position, transform.TransformDirection(-Vector2.right), 1f, 6);
        Debug.DrawRay(transform.position, transform.TransformDirection(-Vector2.right) * 1f, Color.red);

        if(up) {
            Debug.Log(up.collider);
        }
        if(down) {
            Debug.Log(down.collider);
        }
        if(right) {
            Debug.Log(right.collider);
        }
        if(left) {
            Debug.Log(left.collider);
        }
        */

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
        if(GetWallDirection() != -1) {
            if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
            {
                moveVelocity = -speed;
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
            }
        }
        if(GetWallDirection() != 1) {
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
            if(PlayerIsOnGround()) {
                StartFloorJump();
            }
            else if(IsWallToLeftOrRight()) {
                StartWallJump(-GetWallDirection());
            }
            /*
            if(wallContact == -1) {
                StartWallJump(1);
            }
            else if(wallContact == 1) {
                StartWallJump(-1);
            }
            */
            
        }
        if(releasedJump) {
            StopJump();
        }
        if(!jumping) {
            if(IsWallToLeftOrRight()) {                
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, rigidBody.velocity.y);
                rigidBody.gravityScale = wallClingGravity;
            }
            else {
                rigidBody.gravityScale = gravityScale;
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
        
        //Debug.Log(other.gameObject.transform.position);
        
        rigidBody.velocity = new Vector2(0, 0);
        
        
    }
    public bool PlayerIsOnGround() {
        // 1
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - height), transform.TransformDirection(-Vector2.up) * rayCastLengthCheck, Color.red);

        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up * rayCastLengthCheck, Color.red);

        bool groundCheck3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up * rayCastLengthCheck, Color.red);

        // 2
        if (groundCheck1 || groundCheck2 || groundCheck3) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool IsWallToLeftOrRight() {
        // 1
        bool wallOnleft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right * rayCastLengthCheck, Color.red);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width, transform.position.y), Vector2.right * rayCastLengthCheck, Color.red);
        // 2
        if (wallOnleft || wallOnRight) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool PlayerIsTouchingGroundOrWall() {
        if (PlayerIsOnGround() || IsWallToLeftOrRight()) {
            return true;
        }
        else {
            return false;
        }
    }

    public int GetWallDirection() {
        bool isWallLeft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool isWallRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        if (isWallLeft) {
            return -1;
        }
        else if (isWallRight) {
            return 1;
        }
        else {
            return 0;
        }
    }

}
