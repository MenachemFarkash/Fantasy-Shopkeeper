using UnityEngine;

public class PlayerCarrySystem : MonoBehaviour {
    public Transform hand;
    public GameObject CurrentHeldObject;


    public void AddItemToHand(GameObject newItem) {
        if (hand.childCount == 0) {
            newItem.transform.parent = hand;
            newItem.transform.position = hand.transform.position;
            newItem.transform.rotation = hand.transform.rotation;
            CurrentHeldObject = newItem;
        }
    }

    public void ResetToDefault() {
        CurrentHeldObject = null;
    }
}