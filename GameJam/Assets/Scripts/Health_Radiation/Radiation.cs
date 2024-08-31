using UnityEngine;

public class Radiation : MonoBehaviour
{
    [SerializeField] private float startingRadiation;
    public float maxRadiation;
    public RadiationFloat currentRadiation;
    public PlayerSpawner playerSpawner;

    private Camera myCamera;
    private Animator animator;
    private AudioHandler audioHandler;
    private bool dead;

    private void Awake() {
        //currentRadiation = 0;
        animator = GetComponent<Animator>();
        audioHandler = GetComponent<AudioHandler>();
        myCamera = Camera.main;
    }

    public void AddRadiation(float radiation) {
        currentRadiation.runtimeAmount = Mathf.Clamp(currentRadiation.runtimeAmount + radiation, startingRadiation, maxRadiation);

        if(currentRadiation.runtimeAmount < maxRadiation) {
            audioHandler.Play("PlayerHit");
            animator.SetTrigger("Hit");
            StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
        }
        else {
            if(!dead) {
                StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
                audioHandler.Play("PlayerDie");
                animator.SetTrigger("Die");
                GetComponent<NewPlayerMovement>().enabled = false;
                dead = true;
                Die();
            }            
        }
    }

    void Die()
    {
        transform.position = playerSpawner.runtimeSpawnLocation;
        GetComponent<NewPlayerMovement>().enabled = true;
        dead = false;
        currentRadiation.runtimeAmount = startingRadiation;
    }    

}
