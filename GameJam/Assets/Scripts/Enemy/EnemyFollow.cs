using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float visionDistance;    

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    private Transform target;
    private Collider2D targetCollider;
    private bool playerSeen;

    private Animator animator;
    private SpriteRenderer sr;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        animator = enemy.gameObject.GetComponent<Animator>();
        sr = enemy.gameObject.GetComponent<SpriteRenderer>();
    }

    void Update()
    {        
        if(Vector2.Distance(transform.position, target.position) < visionDistance)
        {
            playerSeen = true;
            animator.SetTrigger("PlayerSeen");
        }
        if (playerSeen)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
            if((transform.position.x - target.position.x) < 0)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }
        }
    }
}
