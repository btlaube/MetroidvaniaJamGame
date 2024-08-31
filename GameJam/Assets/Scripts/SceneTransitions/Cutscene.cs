using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cutscene : MonoBehaviour
{
    public Text textElement;
    [TextArea(3, 10)]
    public string cutsceneText;

    private AudioHandler audioHandler;
    private bool fullText = false;

    LevelLoader levelLoader;
    
    void Awake() 
    {
        levelLoader = LevelLoader.instance;
        audioHandler = GetComponent<AudioHandler>();
    }

    void Start()
    {
        ShowText();
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
            audioHandler.Stop("Typing");
        }
    }

    void ShowText() {
        StopAllCoroutines();
        audioHandler.Stop("Typing");
        StartCoroutine(TypeDialogue(cutsceneText));
    }

    IEnumerator TypeDialogue(string text) {
        audioHandler.Play("Typing");
        textElement.text = "";
        foreach (char letter in text.ToCharArray()) {
            textElement.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        fullText = true;
        audioHandler.Stop("Typing");
    }

    void EndCutscene() {
        fullText = false;
        audioHandler.Stop("Typing");
        levelLoader.GetComponent<LevelLoader>().LoadScene(3);
    }
}
