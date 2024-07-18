using UnityEngine;
using UnityEngine.UI;

public class SFMManager : MonoBehaviour
{
    public Button buyBat;
    public Button buyGecko;
    public Item batDna;
    public Item geckoDna;
    public GameObject batSyringe;
    public GameObject geckoSyringe;

    [SerializeField] private int cost = 5;

    Inventory inventory;
    CanvasGroupScript canvasGroup;

    void Start() {
        inventory = Inventory.instance;
        canvasGroup = CanvasGroupScript.instance;
    }

    void Update() {
        // if (Input.GetKeyUp(KeyCode.X)) {
        //     canvasGroup.HideSFM();
        // }

        if (batDna.runtimeItemAmount >= cost) {
            buyBat.interactable = true;
        }
        else {
            buyBat.interactable = false;
        }

        if(geckoDna.runtimeItemAmount >= cost) {
            buyGecko.interactable = true;
        }
        else {
            buyGecko.interactable = false;
        }
    }

    public void BuyBat() {
        for (int i = 0; i <= cost; i++) {
            inventory.Remove(batDna);
        }
        Instantiate(batSyringe, new Vector2(10, -9), Quaternion.identity);
    }

    public void BuyGecko() {
        for (int i = 0; i <= cost; i++) {
            inventory.Remove(geckoDna);
        }
        Instantiate(geckoSyringe, new Vector2(10, -9), Quaternion.identity);
    }

    public void ShowSFMCanvas() {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void HideSFMCanvas() {
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
