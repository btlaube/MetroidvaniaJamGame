using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float visionDistance;    
    [SerializeField] private float damage;

    private Transform target;
    private Collider2D targetCollider;
    private bool playerSeen;
    private Animator animator;
    private SpriteRenderer sr;
    private float attackCooldown;
    private float attackCooldownThreshold = 1f;

    void Awake() {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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
        if(GetComponent<Collider2D>().IsTouching(targetCollider)) {
            if(attackCooldown >= attackCooldownThreshold) {
                target.gameObject.GetComponent<Radiation>().AddRadiation(damage);
                attackCooldown = 0f;
            }
        }
        attackCooldown += Time.deltaTime;
    }
}
