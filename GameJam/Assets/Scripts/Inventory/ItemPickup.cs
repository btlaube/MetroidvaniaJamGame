using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    [SerializeField] private SpriteRenderer spriteRenderer;
    private AudioManager audioManager;

    void Awake() {
        spriteRenderer.sprite = item.icon;
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        
    }

    void OnCollisionEnter2D(Collision2D other) {        
        if(other.gameObject.tag == "Player") {
            PickUp();
        }
    }

    void PickUp() {
        bool wasPickedUp = Inventory.instance.Add(item);
        
        if(wasPickedUp) {
            audioManager.Play("DnaCollect");
            Destroy(gameObject);
        }
    }
}
