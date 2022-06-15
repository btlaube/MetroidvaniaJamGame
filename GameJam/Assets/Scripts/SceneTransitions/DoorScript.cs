using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public int sceneToLoad;
    public int spawnerIndex;
    //public PlayerSpawner playerSpawner;

    [SerializeField] private Transform player;

    LevelLoader levelLoader;

    void Awake() {
        player = GameObject.Find("Player").transform;
    }

    void Start() {
        levelLoader = LevelLoader.instance;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {            
            //set player position
            
            //load scene
            levelLoader.LoadScene(sceneToLoad, spawnerIndex);
            //player.position = playerSpawner.spawnLocation;
        }
    }
}
