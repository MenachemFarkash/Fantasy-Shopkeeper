using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : Interactable {
    public GameObject itemPrefab; // Prefab of the item to display

    public List<Vector3> itemsPositions8 = new List<Vector3>();
    public List<Vector3> itemsPositions10 = new List<Vector3>();
    public List<Vector3> itemsPositions16 = new List<Vector3>();


    public TextMeshPro itemQuantityText;
    public TextMeshPro itemPriceText;

    private int itemQuantity = 0;
    private float itemPrice = 0f;

    public PlayerCarrySystem carrySystem;

    private void Start() {
        carrySystem = PlayerManager.instance.Player.GetComponent<PlayerCarrySystem>();
    }

    public override void Interact() {
        if (carrySystem.CurrentHeldObject != null) {
            base.Interact();

            itemPrefab = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.model;
            itemQuantity = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.amountInCrate;
            itemPrice = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.buyPrice;
            AddItems(itemQuantity, itemPrice);
        }
    }

    public void AddItems(int quantity, float price) {
        itemQuantity = quantity;
        itemPrice = price;

        UpdateDisplay();
        carrySystem.ResetToDefault();
    }

    public void RemoveItems(int quantity) {
        itemQuantity -= Mathf.Clamp(quantity, 0, itemQuantity);
        UpdateDisplay();
    }

    private void UpdateDisplay() {
        itemQuantityText.text = "Quantity: " + itemQuantity;
        itemPriceText.text = "Price: " + itemPrice.ToString("C");

        // Update the visual representation of items on the shelf
        // For example, you can instantiate item prefabs and position them on the shelf
        // based on the quantity.
        // Here's a basic example:
        foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }

        switch (itemQuantity) {
            case 8:
                foreach (Vector3 pos in itemsPositions8) {
                    GameObject itemInstance = Instantiate(itemPrefab, transform);
                    itemInstance.transform.localPosition = pos;
                    Destroy(carrySystem.CurrentHeldObject.gameObject);
                }
                break;
            case 10:
                foreach (Vector3 pos in itemsPositions10) {
                    GameObject itemInstance = Instantiate(itemPrefab, transform);
                    itemInstance.transform.localPosition = pos;
                    Destroy(carrySystem.CurrentHeldObject.gameObject);
                }
                break;
            case 16:
                foreach (Vector3 pos in itemsPositions16) {
                    GameObject itemInstance = Instantiate(itemPrefab, transform);
                    itemInstance.transform.localPosition = pos;
                    Destroy(carrySystem.CurrentHeldObject.gameObject);
                }
                break;
            default:
                break;
        }
    }
}