using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    public float speed;
    public float visionDistance;
    public float width;
    public float height;
    public float widthOffset;
    public float heightOffset;

    [SerializeField] private float damage;

    private Transform target;
    private bool playerSeen;
    private Animator animator;
    private SpriteRenderer sr;
    private float rayCastLengthCheck = 0.025f;
    private float attackCooldown;
    private float attackCooldownThreshold = 1f;

    void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        
        width = GetComponent<Collider2D>().bounds.extents.x + 0.001f;
        height = GetComponent<Collider2D>().bounds.extents.y + 0.001f;
        widthOffset = GetComponent<Collider2D>().offset.x;
        heightOffset = GetComponent<Collider2D>().offset.y;
    }

    void Update() {        
        if(Vector2.Distance(transform.position, target.position) < visionDistance) {
            playerSeen = true;
            animator.SetTrigger("PlayerSeen");
        }
        if (playerSeen) {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if((transform.position.x - target.position.x) < 0) {
                sr.flipX = true;
            }
            else {
                sr.flipX = false;
            }
        }
        if(TouchingPlayer()) {
            if(attackCooldown >= attackCooldownThreshold) {
                target.gameObject.GetComponent<Radiation>().AddRadiation(damage);
                attackCooldown = 0f;
            }
        }
        attackCooldown += Time.deltaTime;
    }

    bool TouchingPlayer() {
        RaycastHit2D groundCheck1 = Physics2D.Raycast(new Vector2(transform.position.x + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D groundCheck2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D groundCheck3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f), transform.position.y - height + heightOffset), -Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D up1 = Physics2D.Raycast(new Vector2(transform.position.x + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D up2 = Physics2D.Raycast(new Vector2(transform.position.x + (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D up3 = Physics2D.Raycast(new Vector2( transform.position.x - (width - 0.2f) + widthOffset, transform.position.y + height + heightOffset), Vector2.up, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallLeft1 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + heightOffset), -Vector2.right, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallLeft2 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallLeft3 = Physics2D.Raycast(new Vector2(transform.position.x - width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), -Vector2.right, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallRight1 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + heightOffset), Vector2.right, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallRight2 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y + (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck, 1<<9);
        RaycastHit2D isWallRight3 = Physics2D.Raycast(new Vector2(transform.position.x + width + widthOffset, transform.position.y - (height - 0.2f) + heightOffset), Vector2.right, rayCastLengthCheck, 1<<9);

        if(groundCheck1 || groundCheck2 || groundCheck3 || up1 || up2 || up3 || isWallLeft1 || isWallLeft2 || isWallLeft3 || isWallRight1 || isWallRight2 || isWallRight3) {
            return true;
        }
        else return false;

    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            other.transform.gameObject.GetComponent<Radiation>().AddRadiation(damage);
        }
    }
}
