using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    Inventory inventory;

    void OnDisable()
    {
        inventory.onItemChangedCallback -= UpdateCanvas;
    }

    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateCanvas;

        UpdateCanvas();
    }

    void UpdateCanvas() {
        InventorySlot[] slots = transform.GetComponentsInChildren<InventorySlot>();

        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
                slots[i].itemAmountText.enabled = true;
                slots[i].itemAmountText.text = inventory.items[i].runtimeItemAmount.ToString();
            }
            else {
                slots[i].ClearSlot();
                slots[i].itemAmountText.enabled = false;
            }
        }
    }
}
