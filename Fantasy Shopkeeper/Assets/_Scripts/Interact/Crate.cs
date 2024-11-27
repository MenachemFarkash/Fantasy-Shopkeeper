using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable {

    public Item crateItemSO;

    public bool canBePickedUp = true;
    public int crateSize;
    public List<Item> itemsInCrate;

    public Transform itemsPositionsContainer;
    public List<Transform> itemsPositions;

    public HeldObjectsType HeldObjectsType = HeldObjectsType.Crate;

    public GameObject crateHologram;





    private void Start() {
        itemsInCrate = new List<Item>(crateItemSO.amountInCrate);

        crateSize = crateItemSO.amountInCrate;
        for (int i = 0; i < crateSize; i++) {
            itemsInCrate.Add(crateItemSO);
        }

        foreach (Transform place in itemsPositionsContainer.GetComponentsInChildren<Transform>()) {
            if (place.childCount <= 0) {
                itemsPositions.Add(place);
            }
        }

        for (int i = 0; i < crateSize; i++) {
            Instantiate(crateItemSO.model, itemsPositions[i]);
        }
    }

    public override void Interact() {
        base.Interact();

        if (canBePickedUp) {
            PlayerManager.instance.Player.GetComponent<PlayerCarrySystem>().AddItemToHand(gameObject);
        }
    }
}

public enum CrateItem {
    None,
    Block
}
