using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RadiationBar : MonoBehaviour
{
    [SerializeField] private Radiation playerRadiation;
    [SerializeField] private Image totalRadiationBar;
    [SerializeField] private Image currentRadiationBar;

    void Update() {
        if(SceneManager.GetActiveScene().buildIndex >= 2) {
            playerRadiation = GameObject.Find("Player").GetComponent<Radiation>();
        }
        if(playerRadiation) {
            totalRadiationBar.fillAmount = playerRadiation.maxRadiation / 10;
            currentRadiationBar.fillAmount = playerRadiation.currentRadiation.runtimeAmount / 10;
        }
        
    }

}
