using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int health;
    public float hitRecoil = 1f;
    public bool isTakingDamage;

    public float width;
    public float height;
    public float widthOffset;
    public float heightOffset;

    private Animator animator;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float rayCastLengthCheck = 0.025f;

    public static Player instance;

    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
        widthOffset = GetComponent<Collider2D>().offset.x;
        heightOffset = GetComponent<Collider2D>().offset.y;

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if(PlayerTouchingEnemy() && !isTakingDamage) {
            TakeDamage();
        }
    }

    void FixedUpdate() {
        if(isTakingDamage) {
            if(sr.flipX) {
                rb.AddForce(new Vector2(((1 * GetComponent<NewPlayerMovement>().speed) - rb.velocity.x) * GetComponent<NewPlayerMovement>().accel, 0));
            }
            else {
                rb.AddForce(new Vector2(((-1 * GetComponent<NewPlayerMovement>().speed) - rb.velocity.x) * GetComponent<NewPlayerMovement>().accel, 0));
            }
        }        
    }

    public bool PlayerTouchingEnemy() {
        
        bool down1 = Physics2D.Raycast(new Vector2(transform.position.x + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, ~7);
        bool down2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, ~7);
        bool down3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, ~7);
        bool left1 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + heightOffset), -Vector2.right, rayCastLengthCheck, ~7);
        bool left2 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck, ~7);
        bool left3 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck, ~7);
        bool right1 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + heightOffset), Vector2.right, rayCastLengthCheck, ~7);
        bool right2 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck, ~7);
        bool right3 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck, ~7);

        if (down1 || down2 || down3 || left1 || left2 || left3 || right1 || right2 || right3) {
            return true;
        }
        else {
            return false;
        }
    }

    public void TakeDamage() {
        isTakingDamage = true;
        health -= 1;
        animator.SetBool("TakingDamage", true);
    }

    public void DoneTakingDamage() {
        isTakingDamage = false;
        animator.SetBool("TakingDamage", false);
    }

}
