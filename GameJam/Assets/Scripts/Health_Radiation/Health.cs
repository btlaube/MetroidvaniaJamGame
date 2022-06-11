using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    public GameObject enemyDrop;

    private AudioManager audioManager;
    private Animator animator;
    private bool dead;
    [SerializeField] private Behaviour[] components;

    private void Awake() {
        currentHealth = 0;
        animator = GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0) {
            audioManager.Play("PlayerHit");
        }
        else {
            if(!dead) {
                audioManager.Play("PlayerDie");
                animator.SetTrigger("Die");

                GetComponent<Collider2D>().enabled = false;
                if(GetComponentInParent<EnemyPatrol>() != null) {
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                }                
                foreach(Behaviour comp in components) {
                    comp.enabled = false;
                }

                dead = true;

                Instantiate(enemyDrop, transform.position, Quaternion.identity);
            }            
        }
    }

    public void Deactivate() {
        if(transform.parent != null) {
            Destroy(transform.parent.gameObject);
        }
        Destroy(gameObject);
    }

}
