using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManagerScript : MonoBehaviour
{
    public SceneBounds screenBounds;
    public PlayerSpawner playerSpawner;

    [SerializeField] private Transform player;

    new CameraFollow camera;

    void Awake() {
        player = GameObject.Find("Player").transform;
        camera = GameObject.Find("Main Camera").GetComponent<CameraFollow>();
    }

    void Start() {
        // camera = CameraFollow.instance;
        camera.minX = screenBounds.minX;
        camera.minY = screenBounds.minY;
        camera.maxX = screenBounds.maxX;
        camera.maxY = screenBounds.maxY;
        SetPlayerLocation();
    }

    public void SetPlayerLocation() {
        player.position = playerSpawner.runtimeSpawnLocation;
    }
}
