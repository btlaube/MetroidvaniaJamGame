using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackCooldownThreshold = 1f;
    [SerializeField] private float damage;
    private float attackCooldown;
    private Transform target;
    private Collider2D targetCollider;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        targetCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
    }

    void Update()
    {    
        if(GetComponent<Collider2D>().IsTouching(targetCollider))
        {
            if(attackCooldown >= attackCooldownThreshold)
            {
                target.gameObject.GetComponent<Radiation>().AddRadiation(damage);
                attackCooldown = 0f;
            }
        }
        attackCooldown += Time.deltaTime;
    }
}
