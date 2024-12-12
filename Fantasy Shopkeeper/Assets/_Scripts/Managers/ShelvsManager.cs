using System.Collections.Generic;
using UnityEngine;

public class ShelvsManager : MonoBehaviour {
    public List<ShelfsContainer> shelfsContainers;
    public List<Shelf> shelfs;
    public List<Item> items;

    public GameObject shelfsContainersContainer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
        foreach (ShelfsContainer shelfsContainer in shelfsContainersContainer.GetComponentsInChildren<ShelfsContainer>()) {
            AddContainerToList(shelfsContainer);
            CheckForFullShelfsInContainer(shelfsContainer);
        }
    }

    public void AddContainerToList(ShelfsContainer shelfsContainer) {
        if (!shelfsContainers.Contains(shelfsContainer)) {
            shelfsContainers.Add(shelfsContainer);
        }
    }

    public void RemoveContainerFromList(ShelfsContainer shelfsContainer) {
        shelfsContainers.Remove(shelfsContainer);
    }

    public void CheckForFullShelfsInContainer(ShelfsContainer shelfsContainer) {
        bool isContainerEmpty = false;
        foreach (Shelf shelf in shelfsContainer.shelves) {
            if (shelf.itemQuantity <= 0) {
                isContainerEmpty = true;
            } else {
                isContainerEmpty = false;
                return;
            }
        }

        if (isContainerEmpty) {
            RemoveContainerFromList(shelfsContainer);
        }
    }

    public void AddShelfToList(Shelf shelf) {
        shelfs.Add(shelf);
        AddItemToList(shelf.item);
    }

    public void RemoveShelfFromList(Shelf shelf) {
        shelfs.Remove(shelf);
        RemoveItemFromList(shelf.item);
        CheckForFullShelfsInContainer(shelf.shelfsContainer);
    }

    public void AddItemToList(Item item) {
        items.Add(item);
    }

    public void RemoveItemFromList(Item item) {
        items.Remove(item);
    }
}
