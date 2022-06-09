using UnityEngine;

public class Radiation : MonoBehaviour
{
    [SerializeField] private float startingRadiation;
    public float maxRadiation;
    public float currentRadiation {get; private set;}

    private Animator animator;

    private void Awake() {
        currentRadiation = 0;
        animator = GetComponent<Animator>();
    }

    public void AddRadiation(float radiation) {
        currentRadiation = Mathf.Clamp(currentRadiation + radiation, startingRadiation, maxRadiation);

        if(currentRadiation > maxRadiation) {
            //player hurt
            animator.SetTrigger("Hit");
        }
        else {
            //animator.SetTrigger("Die");
        }
    }


}
