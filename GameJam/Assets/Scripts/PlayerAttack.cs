using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    private Animator animator;
    private Vector2 input;
    private bool isAttacking;


    void Awake() {
        animator = GetComponent<Animator>();
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
            StartAttack();
        }
    }

    public void StartAttack() {
        isAttacking = true;
        animator.SetBool("IsAttacking", true);
    }

    public void DoneAttacking() {
        isAttacking = false;
        animator.SetBool("IsAttacking", false);
        //isAttacking = false;
        //input.x = 0f;
    }

}
