using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Text textElement;
    public string cutsceneText;

    private AudioManager audioManager;
    private bool fullText = false;

    LevelLoader levelLoader;
    
    public void OnEnable() {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ShowText();
    }

    void Awake() {
        levelLoader = LevelLoader.instance;
    }

    void Update() {
        if(Input.GetKeyUp("return") && !fullText) {
            StopAllCoroutines();
            textElement.text = cutsceneText;
            fullText = true;
        }
        else if (Input.GetKeyUp("return")) {
            EndCutscene();
        }
        if (fullText) {
            audioManager.Stop("Typing");
        }
    }

    void ShowText() {
        StopAllCoroutines();
        audioManager.Stop("Typing");
        StartCoroutine(TypeDialogue(cutsceneText));
    }

    IEnumerator TypeDialogue(string text) {
        audioManager.Play("Typing");
        textElement.text = "";
        foreach (char letter in text.ToCharArray()) {
            textElement.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        fullText = true;
        audioManager.Stop("Typing");
    }

    void EndCutscene() {
        fullText = false;
        audioManager.Stop("Typing");
        levelLoader.GetComponent<LevelLoader>().LoadScene(2, 0);
    }
}
