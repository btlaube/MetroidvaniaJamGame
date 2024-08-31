using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneAudioManager : MonoBehaviour
{
    AudioHandler audioHandler;

    void Awake()
    {
        audioHandler = GetComponent<AudioHandler>();
    }

    private void Start()
    {
        UpdateAudio();
    }

    private void UpdateAudio()
    {
        Scene curScene = SceneManager.GetActiveScene();
        Debug.Log(curScene.buildIndex);
        if (curScene.buildIndex == 0)
        {
            audioHandler.Stop("Game Music");
            audioHandler.Play("Menu Music");
        }
        else if (curScene.buildIndex == 3)
        {
            audioHandler.Stop("Menu Music");
            audioHandler.Play("Game Music");
        }
    }
}
