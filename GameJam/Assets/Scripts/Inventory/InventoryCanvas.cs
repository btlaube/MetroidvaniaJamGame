using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public Transform itemsParent;

    Inventory inventory;

    public InventorySlot[] slots;

    void Start() {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateCanvas;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateCanvas();
    }

    void UpdateCanvas() {
        Debug.Log("updating canvas");
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < inventory.items.Count) {
                slots[i].AddItem(inventory.items[i]);
            }
            //else {
            //    slots[i].ClearSlot();
            //}
        }
    }
}
