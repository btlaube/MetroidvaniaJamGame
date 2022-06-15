using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField] private float damage;
    
    void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Radiation>().AddRadiation(damage);
        }
    }
}
