using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRecoil;
    public AudioManager am;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 input;
    private bool isAttacking;

    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input.x = Input.GetAxis("Fire1");
        //Debug.Log(input.x);
        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) && !isAttacking) {
            Attack();
        }
    }

    public void Attack() {
        am.Play("Attack");
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        rb.position -= new Vector2(attackRecoil, 0);
    }

    public void DoneAttacking() {
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        //isAttacking = false;
        //input.x = 0f;
    }
}
