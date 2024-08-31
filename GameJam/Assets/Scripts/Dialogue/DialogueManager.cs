using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour {

    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Animator dialogueAnimator;
    
    [SerializeField] private NewPlayerMovement playerMovement;
    private Queue<string> sentences;    

    private AudioHandler audioHandler;

    void Awake()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    void Start() {
        sentences = new Queue<string>();
    }

    void Update()
    {
        //TODO: Rename player movement script name if it changes
        playerMovement = GameObject.Find("Player").GetComponent<NewPlayerMovement>();
        if(Input.GetKeyUp(KeyCode.Return))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueAnimator.SetTrigger("Open");
        playerMovement.enabled = false;

        nameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
        }
    }

    IEnumerator TypeSentence (string sentence)
    {
        audioHandler.Play("Typing");
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }
        audioHandler.Stop("Typing");
    }

    void EndDialogue()
    {
        dialogueAnimator.SetTrigger("Close");
        playerMovement.enabled = true;
    }
}
