using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gecko : MonoBehaviour {
    
    [SerializeField] private float damage;
    
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            other.gameObject.GetComponent<Radiation>().AddRadiation(damage);
        }
    }
}
