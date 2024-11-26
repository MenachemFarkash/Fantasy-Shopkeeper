using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    #region Singleton
    public static Inventory Instance;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.LogWarning("More than one instance of inventory found");
        }
    }
    #endregion

    public Transform CamPos;

    public Item currentSelectedItem;

    public int space = 20;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();


    public bool AddItem(Item item) {
        if (!item.isDefaultItem) {

            if (items.Count >= space) {
                Debug.Log("Can't carry any more");
                return false;
            }

            items.Add(item);

            if (onItemChangedCallback != null) {
                onItemChangedCallback.Invoke();
            }
        }
        return true;

    }

    public void RemoveItem(Item item) {
        items.Remove(item);

        if (onItemChangedCallback != null) {
            onItemChangedCallback.Invoke();
        }
    }


}
