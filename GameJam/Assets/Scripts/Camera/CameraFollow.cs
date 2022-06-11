using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraFollow : MonoBehaviour
{
    public Transform camTarget;
    public float trackingSpeed;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    public static CameraFollow instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }

    void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 1) {
            camTarget = GameObject.Find("Player").transform;
        }
    }

    void FixedUpdate() {
        if (camTarget != null && SceneManager.GetActiveScene().buildIndex != 0 && SceneManager.GetActiveScene().buildIndex != 2) {
            var newPos = Vector2.Lerp(transform.position, camTarget.position, Time.deltaTime * trackingSpeed);
            var camPosition = new Vector3(newPos.x, newPos.y, -10f);
            var v3 = camPosition;
            var clampX = Mathf.Clamp(v3.x, minX, maxX);
            var clampY = Mathf.Clamp(v3.y, minY, maxY);
            transform.position = new Vector3(clampX, clampY, -10f);
        }
    }
}
