using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shelf : Interactable {
    public GameObject itemPrefab; // Prefab of the item to display
    public Item item;

    public ShelvsManager shelvsManager;
    public ShelfsContainer shelfsContainer;

    public List<Vector3> itemsPositions8 = new List<Vector3>();
    public List<Vector3> itemsPositions10 = new List<Vector3>();
    public List<Vector3> itemsPositions16 = new List<Vector3>();

    public List<GameObject> gameObjectsOnShelf = new List<GameObject>();


    public TextMeshPro itemQuantityText;
    public TextMeshPro itemPriceText;

    public int itemQuantity = 0;
    public float itemPrice = 0f;

    public PlayerCarrySystem carrySystem;
    public GoldManager goldManager;

    private void Start() {
        shelvsManager = FindAnyObjectByType<ShelvsManager>();
        carrySystem = PlayerManager.instance.Player.GetComponent<PlayerCarrySystem>();
        goldManager = FindAnyObjectByType<GoldManager>();
    }

    public override void Interact() {
        if (carrySystem.CurrentHeldObject != null && itemQuantity == 0) {
            base.Interact();

            itemPrefab = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.model;
            itemQuantity = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.amountInCrate;
            itemPrice = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO.buyPrice;
            item = carrySystem.CurrentHeldObject.GetComponent<Crate>().crateItemSO;
            AddItems(itemQuantity, itemPrice);

            shelvsManager.AddContainerToList(shelfsContainer);
            shelvsManager.AddShelfToList(this);

            return;
        } else if (carrySystem.CurrentHeldObject == null && itemQuantity > 0) {
            base.Interact();
            BuyItemFromShelf(1);
            if (itemQuantity <= 0) {
                shelvsManager.RemoveShelfFromList(this);
            }
        }
    }

    public void AddItems(int quantity, float price) {
        itemQuantity = quantity;
        itemPrice = price;
        PlaceItemsOnShelf();
        UpdateDisplay();
        carrySystem.ResetToDefault();
    }

    private void UpdateDisplay() {
        itemQuantityText.text = "Quantity: " + itemQuantity;
        itemPriceText.text = "Price: " + itemPrice.ToString("C");
    }
    public void PlaceItemsOnShelf() {
        List<Vector3> positions = new List<Vector3>();
        switch (itemQuantity) {
            case 8:
                positions = itemsPositions8;
                break;
            case 10:
                positions = itemsPositions10; ;
                break;
            case 16:
                positions = itemsPositions16; ;
                break;
        }

        foreach (Vector3 pos in positions) {
            GameObject itemInstance = Instantiate(itemPrefab, transform);
            itemInstance.transform.localPosition = pos;
            gameObjectsOnShelf.Add(itemInstance);

            Destroy(carrySystem.CurrentHeldObject.gameObject);
        }
    }

    public void BuyItemFromShelf(int buyAmount) {
        print("Customer Buying");
        for (int i = 0; i < buyAmount; i++) {
            itemQuantity--;
            Destroy(gameObjectsOnShelf[itemQuantity]);
            gameObjectsOnShelf.Remove(gameObjectsOnShelf[itemQuantity]);
            goldManager.AddGold(5);
            UpdateDisplay();
            if (itemQuantity <= 0) {
                shelvsManager.RemoveShelfFromList(this);
            }
        }
    }


}