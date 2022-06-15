using UnityEngine;

public class Collectible : MonoBehaviour
{
    private AudioManager audioManager;

    void Awake() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnCollisionEnter2D(Collision2D other) {        
        if(other.gameObject.tag == "Player") {
            PickUp();
            audioManager.Play("DnaCollect");
            Destroy(gameObject);
        }
    }

    void PickUp() {}
}
