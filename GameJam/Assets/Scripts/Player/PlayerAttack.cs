using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float attackRecoil;
    public AudioManager audioManager;

    private Animator animator;
    private Rigidbody2D rb;
    private Vector2 input;
    [SerializeField] private bool isAttacking;

    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void Update() {
        input.x = Input.GetAxis("Fire1");
        if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.K)) && !isAttacking) {
            Attack();
        }
    }

    public void Attack() {
        transform.GetChild(0).gameObject.SetActive(true);
        audioManager.Play("Attack");
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
        animator.SetTrigger("Attack");
        rb.position -= new Vector2(attackRecoil, 0);
    }

    public void DoneAttacking() {
        transform.GetChild(0).gameObject.SetActive(false);
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
    }
}
