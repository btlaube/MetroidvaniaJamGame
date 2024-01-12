using UnityEngine;

public class PlaceReactorCore : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Sprite withReactorCore;
    public Item reactorCore;

    Inventory inventory;
    AudioManager audioManager;
    LevelLoader levelLoader;

    void Start() {
        inventory = Inventory.instance;
        audioManager = AudioManager.instance;
        levelLoader = LevelLoader.instance;
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.X)) {
            Debug.Log("if you have the core, you win");
            //check if player has reactor core
            //trigger win game sequence
            if(inventory.items.Contains(reactorCore)) {
                Debug.Log("You win");
                audioManager.Play("DnaCollect");
                spriteRenderer.sprite = withReactorCore;
                levelLoader.LoadWinScene();
            }
            else {
                Debug.Log("no core");
            }
        }
    }
}
