using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasGroupScript : MonoBehaviour
{
    public static CanvasGroupScript instance;

    //private bool inventoryOpen;
    private AudioManager audioManager;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
    
        DontDestroyOnLoad(gameObject);

        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
    }

    //void Update() {
    //    if(Input.GetKeyUp(KeyCode.P)) {
    //        audioManager.Play("DnaCollect");
    //        if(inventoryOpen) {
    //            HideInventory();
    //        }
    //        else {
    //            ShowInventory();
    //        }
    //    }
    //}

    public void LoadMainMenu() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void LoadGameScene() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
    }

    public void LoadIntroCutscene() {
        foreach(Transform canvas in transform) {
            canvas.gameObject.SetActive(false);
        }
        transform.GetChild(2).gameObject.SetActive(true);
    }

    //public void ShowInventory() {
    //    inventoryOpen = true;
    //    transform.GetChild(3).gameObject.SetActive(true);
    //}
//
    //public void HideInventory() {
    //    inventoryOpen = false;
    //    transform.GetChild(3).gameObject.SetActive(false);
    //}
}
