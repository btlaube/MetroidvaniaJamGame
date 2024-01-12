using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolVertical : MonoBehaviour {
    
    [Header ("Patrol Points")]
    [SerializeField] private Transform bottomEdge;
    [SerializeField] private Transform topEdge;

    [Header ("Enemy")]
    [SerializeField] private Transform enemy;

    [Header ("Movement Parameters")]
    [SerializeField] private float speed;
    private Vector2 initScale;
    private bool movingDown;

    [Header ("Animator")]
    private Animator animator;

    void Awake() {
        initScale = enemy.localScale;
    }

    void Update() {
        if(movingDown) {
            if(enemy.position.y >= bottomEdge.position.y) {
                MoveInDirection(-1);
            }
            else {
                DirectionChange();
            }
        }
        else {
            if(enemy.position.y <= topEdge.position.y) {
                MoveInDirection(1);
            }
            else {
                DirectionChange();
            }
        }        
    }

    private void DirectionChange() {
        movingDown = !movingDown;
    }

    private void MoveInDirection(int direction) {        
        //Make enemy face direction
        enemy.localScale = new Vector2(Mathf.Abs(initScale.x) * -direction, initScale.y);

        //Move enemy
        enemy.position = new Vector2(enemy.position.x, enemy.position.y + Time.deltaTime * direction * speed);
    }
}
