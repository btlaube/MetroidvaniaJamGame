using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float gravityScale;    
    
    [SerializeField]
    private bool isFalling;
    private Collider2D rb;    
    public Collider2D other;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Collider2D>();
        //isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.IsTouching(other)) {
            isFalling = false;
        }
        else {
            isFalling = true;
        }
    }

    void FixedUpdate() {
        if(isFalling) {
            StartFall();
        }
        else {
            StopFall();
        }
    }

    public void StartFall() {
        this.transform.position -= new Vector3(0, 0.01f, 0);
    }

    public void StopFall() {
        isFalling = false;
    }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Floor") {
            isFalling = false;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Floor") {
            isFalling = true;
        }
    }

}
