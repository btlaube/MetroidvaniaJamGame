using UnityEngine;

public class ShowInteractBar : MonoBehaviour
{
    [SerializeField] private float interactDistance;
    private Transform target;

    void Awake() {
        target = GameObject.Find("Player").transform;
    }

    void Update() {
        if(Vector2.Distance(transform.position, target.position) < interactDistance) {
            transform.GetChild(0).gameObject.SetActive(true);
        }
        else {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
