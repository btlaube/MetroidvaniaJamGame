using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int sceneToLoad;
    public Vector2 nextLocation;
    public PlayerSpawner playerSpawner;


    LevelLoader levelLoader;

    void Start() {
        levelLoader = LevelLoader.instance;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            playerSpawner.runtimeSpawnLocation = nextLocation;
            levelLoader.LoadScene(sceneToLoad);
        }
    }
}
