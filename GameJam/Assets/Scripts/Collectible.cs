using UnityEngine;

public class Collectible : MonoBehaviour
{
    private AudioManager audioManager;

    void Awake() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            audioManager.Play("DnaCollect");
            gameObject.SetActive(false);
        }
    }
}
