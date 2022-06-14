using UnityEngine;

public class SFMTrigger : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    CanvasGroupScript canvasGroup;

    void OnEnable() {
        spriteRenderer.enabled = true;
    }

    void Start() {
        canvasGroup = CanvasGroupScript.instance;
    }

    void Update() {
        if (Input.GetKeyUp(KeyCode.X)) {
            TriggerSFM();
            spriteRenderer.enabled = false;
        }
    }

    public void TriggerSFM() {
        canvasGroup.ShowSFM();
    }
}
