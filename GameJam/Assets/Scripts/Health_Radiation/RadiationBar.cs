using UnityEngine;
using UnityEngine.UI;

public class RadiationBar : MonoBehaviour
{
    [SerializeField] private Radiation playerRadiation;
    [SerializeField] private Image totalRadiationBar;
    [SerializeField] private Image currentRadiationBar;

    void Start() {
        totalRadiationBar.fillAmount = playerRadiation.maxRadiation / 10;
    }

    void Update() {
        currentRadiationBar.fillAmount = playerRadiation.currentRadiation / 10;
    }

}
