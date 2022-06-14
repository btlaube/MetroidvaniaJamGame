using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public Dialogue dialogue;
    
    private DialogueManager dialogueManager;

    void OnEnable() {
        spriteRenderer.enabled = true;
    }

    void Awake() {
        dialogueManager = GameObject.Find("Dialogue Canvas").GetComponent<DialogueManager>();
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.X)) {
            TriggerDialogue();
            spriteRenderer.enabled = false;
        }
    }

    public void TriggerDialogue() {
        dialogueManager.StartDialogue(dialogue);
    }
}
