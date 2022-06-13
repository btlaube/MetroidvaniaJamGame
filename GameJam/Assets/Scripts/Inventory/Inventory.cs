using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

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
    
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    
    public int space = 8;

    public List<Item> items = new List<Item>();

    public bool Add(Item item) {
        if(items.Count >= space) {
            Debug.Log("not enough room.");
            return false;
        }

        if(items.Contains(item)) {
            item.runtimeItemAmount++;
        }
        else {
            items.Add(item);
        }
        
        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();

        return true;
    }

    public void Remove(Item item) {
        if(item.runtimeItemAmount > 1) {
            item.runtimeItemAmount--;
        }
        else {
            items.Remove(item);
        }

        if(onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }
}
