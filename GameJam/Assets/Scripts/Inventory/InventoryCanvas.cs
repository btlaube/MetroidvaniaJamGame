using UnityEngine;

public class InventoryCanvas : MonoBehaviour
{
    public Transform itemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    AudioManager audioManager;

    void Start() {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateCanvas;

        audioManager = AudioManager.instance;
    }

    void Update() {
        if(Input.GetKeyUp(KeyCode.P)) {
            audioManager.Play("DnaCollect");
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            UpdateCanvas();
        }
    }

    void UpdateCanvas() {
        InventorySlot[] slots = itemsParent.GetComponentsInChildren<InventorySlot>();

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
