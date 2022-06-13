using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemAmountText;
    //public Button removeButton;

    Item item;

    void Start() {
        if(!item) {
            itemAmountText.enabled = false;
        }        
    }

    public void AddItem(Item newItem) {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        //removeButton.interactable = false;
    }

    public void ClearSlot() {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void OnRemoveButton() {
        Inventory.instance.Remove(item);
    }

    public void UseItem() {
        if(item != null) {
            item.Use();
        }
    }
}
