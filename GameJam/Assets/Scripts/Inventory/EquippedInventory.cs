using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedInventory : MonoBehaviour
{
    #region Singleton
    public static EquippedInventory instance;

    void Awake() {

        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public delegate void OnEquipmentChanged();
    public OnEquipmentChanged onEquipmentChangedCallback;

    public int space = 4;

    public List<Equipment> currentEquipment = new List<Equipment>();

    public bool Equip(Equipment equipment) {
        if(currentEquipment.Count >= space) {
            Debug.Log("Not enough room");
            return false;
        }
        
        if(currentEquipment.Contains(equipment)) {return false;}
        else {
            currentEquipment.Add(equipment);
        }

        if(onEquipmentChangedCallback != null) {
            onEquipmentChangedCallback.Invoke();
        }

        return true;
    }

    public void Unequip(Equipment equipment) {
        currentEquipment.Remove(equipment);

        if(onEquipmentChangedCallback != null) {
            onEquipmentChangedCallback.Invoke();
        }
    }
}
