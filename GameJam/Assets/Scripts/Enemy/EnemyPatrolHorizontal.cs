using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolHorizontal : MonoBehaviour {
    
    [Header ("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector2 initScale;
    private bool movingLeft;

    [Header ("Animator")]
    private Animator animator;

    void Awake() {
        initScale = enemy.localScale;
    }

    void Update() {
        if(movingLeft) {
            if(enemy.position.x >= leftEdge.position.x) {
                MoveInDirection(-1);
            }
            else {
                DirectionChange();
            }
        }
        else {
            if(enemy.position.x <= rightEdge.position.x) {
                MoveInDirection(1);
            }
            else {
                DirectionChange();
            }
        }        
    }

    private void DirectionChange() {
        movingLeft = !movingLeft;
    }

    private void MoveInDirection(int direction) {        
        //Make enemy face direction
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * direction, initScale.y);

        //Move enemy
        enemy.position = new Vector2(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y);
    }
}
