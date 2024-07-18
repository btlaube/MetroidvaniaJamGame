using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    // public CanvasGroupScript canvasGroup;

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

    // void Start() {
    //     canvasGroup = CanvasGroupScript.instance;
    // }

    public void LoadMainMenu() {
        StartCoroutine(LoadLevel(0));
    }

    public void LoadIntroCutscene() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadWinScene() {
        StartCoroutine(LoadLevel(2));
    }

    public void LoadGameScene() {
        StartCoroutine(LoadLevel(3));
    }

    public void LoadScene(int sceneToLoad) {
        StartCoroutine(LoadLevel(sceneToLoad));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        transition.SetTrigger("End");
    }

    public void Quit() {
        Application.Quit();
    }
}
