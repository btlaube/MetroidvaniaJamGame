using UnityEngine;

public class EquipmentCanvas : MonoBehaviour
{
    public Transform equipmentParent;

    EquippedInventory equipment;

    void OnDisable()
    {
        equipment.onEquipmentChangedCallback -= UpdateCanvas;
    }

    void Start()
    {
        equipment = EquippedInventory.instance;
        equipment.onEquipmentChangedCallback += UpdateCanvas;

        UpdateCanvas();
    }


    void UpdateCanvas() {
        EquipmentSlot[] slots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();

        for (int i = 0; i < slots.Length; i++) {
            if(i < equipment.currentEquipment.Count) {
                slots[i].EquipItem(equipment.currentEquipment[i]);
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }
}
