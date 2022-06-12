using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item item;

    private AudioManager audioManager;

    void Awake() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D other) {        
        if(other.gameObject.tag == "Player") {
            PickUp();
        }
    }

    void PickUp() {
        Debug.Log("Picking Up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item);
        
        if(wasPickedUp) {
            audioManager.Play("DnaCollect");
            Destroy(gameObject);
        }
    }
}
