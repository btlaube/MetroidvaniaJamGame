using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    [SerializeField] private Item item;

    private SpriteRenderer sr;
    private Inventory inventory;

    void Awake()
    {
        inventory = Inventory.instance;
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        sr.sprite = item.icon;
    }

    public void SetItem(Item newItem)
    {
        this.item = newItem;
    }

    public void PickUp()
    {
        inventory.Add(item);
        Destroy(gameObject);
    }
}
