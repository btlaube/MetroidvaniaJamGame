using UnityEngine;

public class Collectible : MonoBehaviour
{
    private AudioHandler audioHandler;

    void Awake() {
        audioHandler = GetComponent<AudioHandler>();
    }

    void OnCollisionEnter2D(Collision2D other) {        
        if(other.gameObject.tag == "Player") {
            PickUp();
            audioHandler.Play("DnaCollect");
            Destroy(gameObject);
        }
    }

    void PickUp() {}
}
