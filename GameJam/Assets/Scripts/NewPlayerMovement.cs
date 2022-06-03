using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerMovement : MonoBehaviour
{
    public float speed = 14f;
    public float accel = 6f;
    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;
    public bool isJumping;
    public float jumpSpeed = 8f;
    private float rayCastLengthCheck = 0.1f;
    private float width;
    private float height;

    public float jumpDurationThreshold = 0.25f;
    private float jumpDuration; 

    public float airAccel = 3f;

    public float jump = 14f;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        //animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
    }

    void Update() {
        // 1
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Jump");
        // 2
        if (rb.velocity.x > 1f) {
            sr.flipX = false;
        }
        else if (rb.velocity.x < -1f) {
            sr.flipX = true;
        }

        if (input.y >= 1f) {
            jumpDuration += Time.deltaTime;
        }
        else {
            isJumping = false;
            jumpDuration = 0f;
        }

        if (PlayerIsOnGround() && isJumping == false) {
            if (input.y > 0f) {
                isJumping = true;
            }
        }

        if (PlayerIsOnWall() && isJumping == false) {
            if (input.y > 0f) {
                isJumping = true;
            }
        }

        //if (jumpDuration > jumpDurationThreshold) input.y = 0f;

        if(PlayerIsOnWall() && !PlayerIsOnGround()) {
            rb.gravityScale = 0.1f;
            if (GetWallDirection() == -1) {
                sr.flipX = false;
            }
            else if (GetWallDirection() == 1) {
                sr.flipX = true;
            }
        }
        else {
            rb.gravityScale = 1f;
        }
    }

    void FixedUpdate() {
        // 1
        var acceleration = 0f;
        if (PlayerIsOnGround()) {
            acceleration = accel;
        }
        else {
            acceleration = airAccel;
        }
        var xVelocity = 0f;
        // 2
        if (PlayerIsOnGround() && input.x == 0) {
            xVelocity = 0f;
        }
        else {
            xVelocity = rb.velocity.x;
        }

        var yVelocity = 0f;
        if (PlayerIsTouchingGroundOrWall() && input.y == 1) {
            yVelocity = jump;
        }
        else {
            yVelocity = rb.velocity.y;
        }

        // 3
        if(GetWallDirection() == -1) {
            if(input.x != -1) {
                rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
                jumpDuration = 0f;
            }
        }
        else if(GetWallDirection() == 1) {
            if(input.x != 1) {
                rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
                jumpDuration = 0f;
            }
        }
        else {
            rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
        }
        // 4
        rb.velocity = new Vector2(xVelocity, yVelocity);

        if(PlayerIsOnWall() && !PlayerIsOnGround() && input.y == 1) {
            rb.velocity = new Vector2(-GetWallDirection() * speed * 0.75f, rb.velocity.y);            
        }

        if (isJumping && jumpDuration < jumpDurationThreshold) {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
        }
    }

    public bool PlayerIsOnGround() {
        // 1
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - height), transform.TransformDirection(-Vector2.up) * rayCastLengthCheck, Color.red);

        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + (width - 0.2f), transform.position.y - height), -Vector2.up * rayCastLengthCheck, Color.blue);

        bool groundCheck3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height), -Vector2.up * rayCastLengthCheck, Color.green);
        // 2
        if (groundCheck1 || groundCheck2 || groundCheck3) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool PlayerIsOnWall() {
        // 1
        bool wallOnleft = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        // 2
        if (wallOnleft || wallOnRight) {
            //rb.velocity = new Vector2(0f, 0f);
            return true;
        }
        else {
            return false;
        }
    }

    public bool PlayerIsTouchingGroundOrWall() {
        if (PlayerIsOnGround() || PlayerIsOnWall()) {
            return true;
        }
        else {
            return false;
        }
    }

    public int GetWallDirection() {
        bool isWallLeft1 = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right * rayCastLengthCheck, Color.red);

        bool isWallLeft2 = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width, transform.position.y + (height - 0.2f)), -Vector2.right * rayCastLengthCheck, Color.green);

        bool isWallLeft3 = Physics2D.Raycast(new Vector2(transform.position.x - width, transform.position.y), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width, transform.position.y - (height - 0.2f)), -Vector2.right * rayCastLengthCheck, Color.blue);

        bool isWallRight1 = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width, transform.position.y), Vector2.right * rayCastLengthCheck, Color.red);

        bool isWallRight2 = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width, transform.position.y + (height - 0.2f)), Vector2.right * rayCastLengthCheck, Color.green);

        bool isWallRight3 = Physics2D.Raycast(new Vector2(transform.position.x + width, transform.position.y), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width, transform.position.y - (height - 0.2f)), Vector2.right * rayCastLengthCheck, Color.blue);

        if (isWallLeft1 || isWallLeft2 || isWallLeft3) {
            return -1;
        }
        else if (isWallRight1 || isWallRight2 || isWallRight3) {
            return 1;
        }
        else {
            return 0;
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(PlayerIsOnWall()) {
            rb.velocity = new Vector2(0f, 0f);
        }
        
    }

}
