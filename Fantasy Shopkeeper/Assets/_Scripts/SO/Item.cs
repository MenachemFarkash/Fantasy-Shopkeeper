using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public int buyPrice;
    public int sellPrice;
    public GameObject model;
    public int amountInCrate;
    public GameObject crate;

    public ItemType type;

    public virtual void Use() {
        // Use the item

        Debug.Log("Using: " + name);
        Inventory.Instance.currentSelectedItem = this;
    }


    public void RemoveFromInventory() {
        Inventory.Instance.RemoveItem(this);
    }
}

public enum ItemType {
    none,
}



