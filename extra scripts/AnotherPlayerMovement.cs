using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnotherPlayerMovement : MonoBehaviour
{
    public float speed;
    public int falling;
    public float gravityScale = -0.05f;
    public float jumpForce = 0.05f;

    [SerializeField]
    private GameObject[] floors;
    [SerializeField]
    private GameObject[] walls;
    [SerializeField]
    private GameObject[] platforms;

    private Collider2D collider;
    private float moveVelocity;
    private bool jumping = false;

    [SerializeField]
    private float jumpTimer = 0.5f;
    private bool startTimer = false;
    private float timer;

    void Awake() {
        collider = GetComponent<Collider2D>();
        floors = GameObject.FindGameObjectsWithTag("Floor");
        walls = GameObject.FindGameObjectsWithTag("Wall");

        platforms = new GameObject[floors.Length + walls.Length];
        floors.CopyTo(platforms, 0);
        walls.CopyTo(platforms, floors.Length);
        gravityScale = CheckTouching();

        timer = jumpTimer;
    }

    void Update() {
        
        if(!jumping) {
            gravityScale = CheckTouching();
        }

        if(Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W)) {
            jumping = true;
            startTimer = true;
            gravityScale = jumpForce;
        }
        if(Input.GetKeyUp (KeyCode.Space) || Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.Z) || Input.GetKeyUp (KeyCode.W)) {
            jumping = false;
            gravityScale = -0.05f;
        }

        if(startTimer) {
            timer -= Time.deltaTime;
            if(timer <= 0) {
                jumping = false;
            }
            timer = jumpTimer;
            startTimer = false;
        }

        moveVelocity = 0f;
        
        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
        {
            moveVelocity -= speed;
            //transform.position += new Vector3(moveVelocity, 0, 0);
        }
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
        {
            moveVelocity += speed;
            //transform.position += new Vector3(moveVelocity, 0, 0);
        }
        
        transform.position += new Vector3(moveVelocity, gravityScale, 0);
    }

    void FixedUpdate() {
        
    }

    void StartFloorJump() {
        
    }

    void StartWallJump(int direction) {
        
    }

    void StopJump() {
        
    }

    float CheckTouching() {
        float result = -0.05f;
        foreach(GameObject platform in platforms) {
            if(collider.IsTouching(platform.GetComponent<Collider2D>())) {
                if(platform.tag == "Floor") {
                    return 0;
                }
                else if(platform.tag == "Wall") {
                    result = -0.01f;
                }
            }
        }
        return result;
    }

}
