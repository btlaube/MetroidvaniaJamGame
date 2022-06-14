using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Animator dialogueAnimator;
    
    [SerializeField] private Animator AiAnimator;
    private Queue<string> sentences;
    [SerializeField] private NewPlayerMovement playerMovement;

    AudioManager audioManager;

    void Start() {
        sentences = new Queue<string>();
        audioManager = AudioManager.instance;
    }

    void Update() {
        if(SceneManager.GetActiveScene().buildIndex == 2) {
            AiAnimator = GameObject.Find("AI").GetComponent<Animator>();
            playerMovement = GameObject.Find("Player").GetComponent<NewPlayerMovement>();
        }        
        if(Input.GetKeyUp(KeyCode.Return)) {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue) {
        dialogueAnimator.SetTrigger("Open");
        AiAnimator.SetBool("Talking", true);
        playerMovement.enabled = false;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }
        else {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence (string sentence) {
        audioManager.Play("Typing");
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        audioManager.Stop("Typing");
    }

    void EndDialogue() {
        dialogueAnimator.SetTrigger("Close");
        AiAnimator.SetBool("Talking", false);
        playerMovement.enabled = true;
    }
}
