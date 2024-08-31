using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEventTrigger : MonoBehaviour
{
    public UnityEvent myEvent;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            // StartCoroutine(Fart());
            myEvent.Invoke();
            Destroy(gameObject);
        }
    }

    public void SelfDestruct()
    {
        Destroy(gameObject);
    }

}
