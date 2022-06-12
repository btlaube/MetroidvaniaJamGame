using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    Inventory inventory;

    void Start() {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateCanvas;
    }

    void UpdateCanvas() {
        Debug.Log("Updating UI");
    }
}
