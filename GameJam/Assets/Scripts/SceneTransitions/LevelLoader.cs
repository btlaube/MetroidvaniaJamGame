using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    public GameObject canvasGroup;

    [SerializeField] private SceneManagerScript sceneManager;
    [SerializeField] private float transitionTime = 1f;

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
        StartCoroutine(LoadLevel(0));
    }

    public void LoadIntroCutscene() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadGameScene() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadScene(int sceneToLoad, int spawnerIndex) {
        StartCoroutine(LoadLevel(sceneToLoad, spawnerIndex));
    }

    IEnumerator LoadLevel(int levelIndex, int spawnerIndex = 0) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        switch(levelIndex) {
            case 0:
                canvasGroup.GetComponent<CanvasGroupScript>().LoadMainMenu();
                break;
            case 1:
                canvasGroup.GetComponent<CanvasGroupScript>().LoadIntroCutscene();
                break;
            case 2:
            case 3:
                canvasGroup.GetComponent<CanvasGroupScript>().LoadGameScene();
                sceneManager = GameObject.Find("SceneManager").GetComponent<SceneManagerScript>();
                sceneManager.SetPlayerLocation(spawnerIndex);
                break;
        }
        transition.SetTrigger("End");
    }

    public void Quit() {
        Application.Quit();
    }
}
