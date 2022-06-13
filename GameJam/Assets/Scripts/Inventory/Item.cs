using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject, ISerializationCallbackReceiver
{
    new public string name = "New Item";
    public Sprite icon = null;

    public int initialItemAmount = 1;
    public int runtimeItemAmount = 1;

    public virtual void Use() {}

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }

    public void OnAfterDeserialize() {
        runtimeItemAmount = initialItemAmount;
    }

    public void OnBeforeSerialize() { }
}
