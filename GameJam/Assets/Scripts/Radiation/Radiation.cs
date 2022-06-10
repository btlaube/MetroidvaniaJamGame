using UnityEngine;

public class Radiation : MonoBehaviour
{
    [SerializeField] private float startingRadiation;
    public float maxRadiation;
    public float currentRadiation {get; private set;}
    public AudioManager am;
    public Camera myCamera;

    private Animator animator;
    private bool dead;

    private void Awake() {
        currentRadiation = 0;
        animator = GetComponent<Animator>();
    }

    public void AddRadiation(float radiation) {
        Debug.Log(currentRadiation);
        currentRadiation = Mathf.Clamp(currentRadiation + radiation, startingRadiation, maxRadiation);

        if(currentRadiation < maxRadiation) {
            am.Play("PlayerHit");
            animator.SetTrigger("Hit");
            StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
        }
        else {
            if(!dead) {
                am.Play("PlayerDie");
                animator.SetTrigger("Die");
                GetComponent<NewPlayerMovement>().enabled = false;
                dead = true;
            }            
        }
    }

    

}
