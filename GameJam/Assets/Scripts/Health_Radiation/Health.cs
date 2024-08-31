using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth {get; private set;}
    public Item dropItem;
    public GameObject itemPickupPrefab;

    private AudioHandler audioHandler;
    private Animator animator;
    private bool dead;
    [SerializeField] private Behaviour[] components;


    private void Awake()
    {
        currentHealth = 0;
        animator = GetComponent<Animator>();
        audioHandler = GetComponent<AudioHandler>();
    }

    public void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0) {
            audioHandler.Play("Hit");
        }
        else {
            if(!dead) {
                audioHandler.Play("Die");
                animator.SetTrigger("Die");

                GetComponent<Collider2D>().enabled = false;

                if(GetComponentInParent<EnemyPatrolHorizontal>() != null) {
                    GetComponentInParent<EnemyPatrolHorizontal>().enabled = false;
                }

                if(GetComponentInParent<EnemyPatrolVertical>() != null) {
                    GetComponentInParent<EnemyPatrolVertical>().enabled = false;
                }

                foreach(Behaviour comp in components) {
                    comp.enabled = false;
                }

                dead = true;

                GameObject enemyDrop = Instantiate(itemPickupPrefab, transform.position, Quaternion.identity);
                itemPickupPrefab.GetComponent<ItemPickup>().SetItem(dropItem);
            }            
        }
    }

    public void Deactivate()
    {
        Destroy(gameObject);
    }

}
