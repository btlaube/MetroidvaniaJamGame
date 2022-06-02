using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public enum State{FALLING, ONWALL, ONFLOOR};

    //private Collider2D collider;

    public float gravityScale;
    public float jumpForce;
    public float speed;
    public float moveVelocity;
    
    [SerializeField]
    private State state;
    [SerializeField]
    private GameObject[] floors;
    [SerializeField]
    private GameObject[] walls;

    void Awake() {
        collider = GetComponent<Collider2D>();
    }

    void Start() {
        floors = GameObject.FindGameObjectsWithTag("Floor");
        walls = GameObject.FindGameObjectsWithTag("Wall");
    }

    void Update() {
        switch(state) {
            case State.FALLING:
                transform.position -= new Vector3(0, gravityScale, 0);

                foreach(GameObject floor in floors) {
                    if(GetComponent<Collider>().IsTouching(floor.GetComponent<Collider2D>())) {
                        state = State.ONFLOOR;
                    }
                }

                foreach(GameObject wall in walls) {
                    if(GetComponent<Collider>().IsTouching(wall.GetComponent<Collider2D>())) {
                        state = State.ONWALL;
                    }
                }

                break;
            case State.ONFLOOR:
                break;
            case State.ONWALL:
                break;
        }

        moveVelocity = 0;

        //Left Right Movement
        if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) 
        {
            moveVelocity = -speed;
            GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
        }
        if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) 
        {
            moveVelocity = speed;
            GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveVelocity, GetComponent<Rigidbody2D> ().velocity.y);
        }

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

    void FixedUpdate() {
        
    }
}
