using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    private Transform player;

    void Awake() {
        player = GameObject.Find("Player").transform;
    }

    void Start() {
        if(player) {
            player.position = transform.position;
        }
    }
}
