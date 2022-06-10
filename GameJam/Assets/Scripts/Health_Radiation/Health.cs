using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    public AudioManager am;

    private Animator animator;
    private bool dead;

    private void Awake() {
        currentHealth = 0;
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0) {
            am.Play("PlayerHit");
        }
        else {
            if(!dead) {
                am.Play("PlayerDie");
                animator.SetTrigger("Die");

                GetComponent<Collider2D>().enabled = false;
                GetComponentInParent<EnemyPatrol>().enabled = false;

                dead = true;
            }            
        }
    }

    public void Deactivate() {
        Destroy(transform.parent.gameObject);
        Destroy(gameObject);
    }

}
