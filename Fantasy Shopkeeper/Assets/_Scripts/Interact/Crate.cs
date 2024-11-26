using System.Collections.Generic;
using UnityEngine;

public class Crate : Interactable {

    public bool canBePickedUp = true;
    public Item crateItem;
    public int crateSize;
    public List<Item> itemsInCrate;

    public Transform itemsPositionsContainer;
    public List<Transform> itemsPositions;

    public HeldObjectsType HeldObjectsType = HeldObjectsType.Crate;

    public GameObject crateHologram;

    private void Awake() {
        itemsInCrate = new List<Item>(crateSize);
    }

    private void Start() {

        for (int i = 0; i < crateSize; i++) {
            itemsInCrate.Add(crateItem);
        }
        foreach (Transform place in itemsPositionsContainer.GetComponentsInChildren<Transform>()) {
            if (place.childCount <= 0) {
                itemsPositions.Add(place);
                Instantiate(crateItem.model, place);
            }
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
