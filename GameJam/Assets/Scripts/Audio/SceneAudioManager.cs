using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    AudioManager audioManager;
    
    private void Start()
    {
        audioManager = AudioManager.instance;
        UpdateAudio();
    }

    private void UpdateAudio()
    {
        Scene curScene = SceneManager.GetActiveScene();
        Debug.Log(curScene.buildIndex);
        if (curScene.buildIndex == 0)
        {
            audioManager.Stop("Game Music");
            audioManager.Play("Menu Music");
        }
        else if (curScene.buildIndex == 3)
        {
            audioManager.Stop("Menu Music");
            audioManager.Play("Game Music");
        }
    }
}
