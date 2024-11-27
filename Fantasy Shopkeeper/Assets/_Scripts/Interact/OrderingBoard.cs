using System.Collections.Generic;
using UnityEngine;

public class OrderingBoard : Interactable {
    public Transform DeliverySpot;

    public GameObject CratePrefab;
    public List<Item> Items;

    public override void Interact() {
        base.Interact();

        Deliver(RollRandomItem());

    }

    public void Deliver(Item item) {
        GameObject newCrate = Instantiate(item.crate, DeliverySpot);
        newCrate.GetComponent<Crate>().crateItemSO = item;
    }

    public Item RollRandomItem() {
        return Items[Random.Range(0, Items.Count)];
    }
}
