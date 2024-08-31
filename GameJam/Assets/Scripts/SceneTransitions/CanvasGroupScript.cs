using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupScript : MonoBehaviour
{
    public static CanvasGroupScript instance;

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

    public void LoadMainMenu() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void LoadIntroCutscene() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(2).gameObject.SetActive(true);
    }

    public void LoadGameScene() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(5).gameObject.SetActive(true);
    }
    
    public void LoadWinScene() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(7).gameObject.SetActive(true);
    }

    public void ShowSFM() {
        transform.GetChild(6).gameObject.SetActive(true);
    }

    public void HideSFM() {
        transform.GetChild(6).gameObject.SetActive(false);
    }
}
