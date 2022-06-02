using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{

//Movement
    public float speed;
    public float jump;
    float moveVelocity;

    [SerializeField]
    int numFloorContacts;
    [SerializeField]
    int numWallContacts;

    //Grounded Vars
    //[SerializeField]
    //bool grounded = true;

    void Update () 
    {
        //Jumping
        /*
        if (Input.GetKeyDown (KeyCode.Space) || Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.W)) 
        {
            if(numWallContacts == -1) {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (jump, jump);
            }
            else if(numWallContacts == 1) {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (-jump, jump);
            }
            else if(numFloorContacts > 0)
            {
                GetComponent<Rigidbody2D> ().velocity = new Vector2 (GetComponent<Rigidbody2D> ().velocity.x, jump);
            }
        }
*/
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
    //Check if Grounded
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Floor") {
            numFloorContacts++;
        }
        else if(other.gameObject.tag == "Wall") {
            if(other.GetComponent<EdgeCollider2D>().points[0].x < this.transform.position.x) {
                numWallContacts = -1;
            }
            else {
                numWallContacts = 1;
            }
        }
        //grounded = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Floor") {
            numFloorContacts--;
        }
        else if(other.gameObject.tag == "Wall") {
            numWallContacts = 0;
        }
        //grounded = false;
    }
}