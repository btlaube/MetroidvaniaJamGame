using UnityEngine;

public class Radiation : MonoBehaviour
{
    [SerializeField] private float startingRadiation;
    public float maxRadiation;
    public float currentRadiation {get; private set;}
    public PlayerSpawner playerSpawner;

    private Camera myCamera;
    private AudioManager audioManager;
    private Animator animator;
    private bool dead;

    private void Awake() {
        currentRadiation = 0;
        animator = GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        myCamera = Camera.main;
    }

    public void AddRadiation(float radiation) {
        currentRadiation = Mathf.Clamp(currentRadiation + radiation, startingRadiation, maxRadiation);

        if(currentRadiation < maxRadiation) {
            audioManager.Play("PlayerHit");
            animator.SetTrigger("Hit");
            StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
        }
        else {
            if(!dead) {
                StartCoroutine(myCamera.GetComponent<CameraShake>().Shake(0.2f, 0.2f));
                audioManager.Play("PlayerDie");
                animator.SetTrigger("Die");
                GetComponent<NewPlayerMovement>().enabled = false;
                dead = true;
                Die();
            }            
        }
    }

    void Die() {
        transform.position = playerSpawner.runtimeSpawnLocation;
        GetComponent<NewPlayerMovement>().enabled = true;
        dead = false;
        currentRadiation = startingRadiation;
    }    

}
