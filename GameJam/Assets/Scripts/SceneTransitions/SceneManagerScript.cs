using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public SceneBounds screenBounds;
    public PlayerSpawner[] playerSpawners;

    [SerializeField] private Transform player;

    new CameraFollow camera;

    void Awake() {
        player = GameObject.Find("Player").transform;
    }

    void Start() {
        camera = CameraFollow.instance;
        camera.minX = screenBounds.minX;
        camera.minY = screenBounds.minY;
        camera.maxX = screenBounds.maxX;
        camera.maxY = screenBounds.maxY;
    }

    public void SetPlayerLocation(int spawnerIndex) {
        player.position = playerSpawners[spawnerIndex].spawnLocation;
    }
}
