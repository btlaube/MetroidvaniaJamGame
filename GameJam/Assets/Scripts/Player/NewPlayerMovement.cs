using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NewPlayerMovement : MonoBehaviour
{
    public float speed = 14f;
    public float accel = 6f;
    public bool isJumping;
    public bool isFalling;
    public float jumpSpeed = 8f;
    public float width;
    public float height;
    public float widthOffset;
    public float heightOffset;
    public float jumpDurationThreshold = 2.25f;
    public float airAccel = 3f;
    public float jump = 14f;
    public AudioManager am;

    private Vector2 input;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Animator animator;   
    private float rayCastLengthCheck = 0.025f;
    private float jumpDuration;

    [SerializeField]
    private bool gecko;
    [SerializeField]
    private bool magnet;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
        widthOffset = GetComponent<Collider2D>().offset.x;
        heightOffset = GetComponent<Collider2D>().offset.y;
    }

    void Update() {   
        if(!(GetComponent<Player>().isTakingDamage)) {
            input.x = Input.GetAxis("Horizontal");
            input.y = Input.GetAxis("Jump");

            animator.SetFloat("Speed", Mathf.Abs(input.x));
            
            if (input.x > 0.1f) {
                sr.flipX = false;
            }
            else if (input.x < -0.1f) {
                sr.flipX = true;
            }

            if (input.y >= 1f && jumpDuration < jumpDurationThreshold) {
                jumpDuration += Time.deltaTime;
                animator.SetBool("IsJumping", true);
            }
            else if(input.y < 1) {
                isJumping = false;
                animator.SetBool("IsJumping", false);
                jumpDuration = 0f;
            }

            if(!PlayerIsOnCeiling() && !PlayerIsOnGround() && !PlayerIsOnWall() && !isJumping) {
                animator.SetBool("IsFalling", true);
            }
            else {
                animator.SetBool("IsFalling", false);
            }

            if (PlayerIsOnGround() && isJumping == false) {
                if (input.y > 0f) {
                    isJumping = true;
                }
                animator.SetBool("IsOnWall", false);
            }

            //Control gravity for wall slide
            if(gecko) {
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
            

            if (jumpDuration > jumpDurationThreshold) input.y = 0f;

            //Control gravity for ceiling cling if Magnet is active
            if(magnet) {
                if(PlayerIsOnCeiling()) {
                    rb.gravityScale = 0f;
                }
                else if(!PlayerIsOnWall()){
                    rb.gravityScale = 1f;
                }
            }
        }        
    }

    void FixedUpdate() {
        if(!(GetComponent<Player>().isTakingDamage)) {
            var acceleration = 0f;
            if (PlayerIsOnGround() || PlayerIsOnCeiling()) {
                acceleration = accel;
            }
            else {
                acceleration = airAccel;
            }

            var xVelocity = 0f;        
            if (PlayerIsOnGround() && input.x == 0) {
                xVelocity = 0f;
            }
            else {
                xVelocity = rb.velocity.x;
            }

            var yVelocity = 0f;
            if (PlayerIsOnGround() && input.y > 0) {
                yVelocity = jump;
                am.Play("Jump");
            }
            else if(PlayerIsOnWall() && input.y > 0 && gecko) {
                yVelocity = jump;
                am.Play("Jump");
            }
            else if(PlayerIsOnCeiling() && input.y > 0) {
                yVelocity = -jump;
                am.Play("Jump");
            }
            else {
                yVelocity = rb.velocity.y;
            }

            //Controls movement on x-axis
            //Prevents movement on x-axis depending on if player is on a wall to prevent player from clinging to wall
            if(GetWallDirection() == -1) {
                if(input.x > 0) {
                    rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
                }
            }
            else if(GetWallDirection() == 1) {
                if(input.x < 0) {
                    rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
                }
            }
            else {
                rb.AddForce(new Vector2(((input.x * speed) - rb.velocity.x) * acceleration, 0));
            }
            
            rb.velocity = new Vector2(xVelocity, yVelocity);

            //Controls movement on x-axis for wall jumping
            if(PlayerIsOnWall() && !PlayerIsOnGround() && input.y == 1 && gecko) {
                rb.velocity = new Vector2(-GetWallDirection() * speed * 0.75f, rb.velocity.y);
                animator.SetBool("IsOnWall", false);
                animator.SetBool("IsJumping", true);
            }
            else if (!PlayerIsOnWall()) {
                animator.SetBool("IsOnWall", false);
                animator.SetBool("IsJumping", true);
            }
            if (PlayerIsOnWall() && !PlayerIsOnGround() && gecko) {
                animator.SetBool("IsOnWall", true);
            }

            if (isJumping && jumpDuration < jumpDurationThreshold) {
                rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            }
            else {
                isJumping = false;
                animator.SetBool("IsJumping", false);
            }
        }        
    }

    public bool PlayerIsOnGround() {
        
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + widthOffset, transform.position.y - height + heightOffset), transform.TransformDirection(-Vector2.up) * rayCastLengthCheck, Color.red);

        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y - height + heightOffset), -Vector2.up * rayCastLengthCheck, Color.blue);

        bool groundCheck3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2( transform.position.x - (width - 0.2f) + widthOffset, transform.position.y - height + heightOffset), -Vector2.up * rayCastLengthCheck, Color.green);
        
        if (groundCheck1 || groundCheck2 || groundCheck3) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool PlayerIsOnWall() {
        
        bool wallOnleft = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + heightOffset), -Vector2.right, rayCastLengthCheck);
        bool wallOnRight = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + heightOffset), Vector2.right, rayCastLengthCheck);
        
        if (wallOnleft || wallOnRight) {
            return true;
        }
        else {
            return false;
        }
    }

    public bool PlayerIsOnCeiling() {
        bool groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + widthOffset, transform.position.y + height + heightOffset), transform.TransformDirection(Vector2.up) * rayCastLengthCheck, Color.red);

        bool groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up * rayCastLengthCheck, Color.blue);

        bool groundCheck3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck);
        Debug.DrawRay(new Vector2( transform.position.x - (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up * rayCastLengthCheck, Color.green);

        if (groundCheck1 || groundCheck2 || groundCheck3) {
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
        bool isWallLeft1 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + heightOffset), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width + widthOffset, transform.position.y + heightOffset), -Vector2.right * rayCastLengthCheck, Color.red);

        bool isWallLeft2 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), -Vector2.right * rayCastLengthCheck, Color.green);

        bool isWallLeft3 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x - width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), -Vector2.right * rayCastLengthCheck, Color.blue);

        bool isWallRight1 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + heightOffset), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width + widthOffset, transform.position.y + heightOffset), Vector2.right * rayCastLengthCheck, Color.red);

        bool isWallRight2 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), Vector2.right * rayCastLengthCheck, Color.green);

        bool isWallRight3 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck);
        Debug.DrawRay(new Vector2(transform.position.x + width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), Vector2.right * rayCastLengthCheck, Color.blue);

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
        if(PlayerIsOnWall() || PlayerIsOnCeiling()) {
            rb.velocity = new Vector2(0f, 0f);
        }
    }

}
