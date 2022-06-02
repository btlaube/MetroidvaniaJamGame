using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 5f;
    private bool pressedJump = false;
    private bool releasedJump = false;
    private Rigidbody2D rb;
    [SerializeField]
    private float gravityScale = 1f;
    [SerializeField]
    private float maxHeight = 3f;
    private float startHeight;

    [SerializeField]
    private float wallCling = 2f;

    [SerializeField]
    private bool falling = false;

    [SerializeField]
    private bool grounded = false;
    [SerializeField]
    private int wallContact;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() {
        Debug.Log(rb.velocity);
        if(Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W)) {
            pressedJump = true;           
        }
        if(Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.Z) || Input.GetKeyUp (KeyCode.W)) {
            releasedJump = true;
        }
        if(transform.position.y - startHeight >= maxHeight) {
            releasedJump = true;
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
    }

    void StartFloorJump() {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        pressedJump = false;
        startHeight = transform.position.y;
    }

    void StartWallJump(int direction) {
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(0, 0);
        rb.AddForce(new Vector2(direction * jumpForce, jumpForce), ForceMode2D.Impulse);
        pressedJump = false;
        startHeight = transform.position.y;
    }

    void StopJump() {
        rb.gravityScale = gravityScale;
        releasedJump = false;
    }

    void StartFall() {
        rb.velocity = new Vector2(0, -gravityScale);
    }

    void StopFall() {
        rb.velocity = new Vector2(0, 0);
        falling = false;
    }

    void OnCollisionEnter2D(Collision other)
    {
        if(other.gameObject.tag == "Floor") {
            grounded = true;
        }
        else if(other.gameObject.tag == "Wall") {
            if(other.GameObject.GetComponent<EdgeCollider2D>().points[0].x < this.transform.position.x) {
                wallContact = -1;
            }
            else {
                wallContact = 1;
            }
        }
        
    }
    void OnCollisionExit2D(Collision other)
    {
        if(other.gameObject.tag == "Floor") {
            grounded = false;
        }
        else if(other.gameObject.tag == "Wall") {
            wallContact = 0;
        }
    }

}
