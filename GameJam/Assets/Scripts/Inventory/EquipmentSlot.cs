using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;

    Equipment equipment;

    public void EquipItem(Equipment newEquipment) {
        Debug.Log("equip item");
        equipment = newEquipment;

        icon.sprite = equipment.icon;
        icon.enabled = true;
    }

    public void ClearSlot() {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UnequipItem() {
        if(equipment != null) {
            equipment.Unequip();
        }
    }
}
