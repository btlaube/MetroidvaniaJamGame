using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AudioManager am;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player") {
            am.Play("DnaCollect");
            gameObject.SetActive(false);
        }
    }
}
