using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    public GameObject canvasGroup;

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

    public void LoadGameScene() {
        StartCoroutine(LoadLevel(1));
    }

    public void LoadMainMenu() {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

        switch(levelIndex) {
            case 0:
                canvasGroup.GetComponent<CanvasGroupScript>().LoadMainMenu();
                break;
            case 1:
                canvasGroup.GetComponent<CanvasGroupScript>().LoadGameScene();
                break;
        }
        transition.SetTrigger("End");
    }

    public void Quit() {
        Application.Quit();
    }
}
