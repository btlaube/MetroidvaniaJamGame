using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public override void Use() {
        base.Use();
        bool wasEquipped = EquippedInventory.instance.Equip(this);
        if(wasEquipped)
            RemoveFromInventory();
    }

    public void Unequip() {
        EquippedInventory.instance.Unequip(this);
    }

}
