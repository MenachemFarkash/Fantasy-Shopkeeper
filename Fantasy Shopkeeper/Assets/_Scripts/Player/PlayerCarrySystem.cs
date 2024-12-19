using UnityEngine;

public class PlayerCarrySystem : MonoBehaviour {
    public Transform hand;
    public GameObject CurrentHeldObject;
    public bool isHoldingObject = false;

    public LayerMask groundLayerMask;

    public GameObject holdingObjectHologram;
    public Vector3 holdingObjectHologramRotation = Vector3.zero;
    public bool isHologramActive = false;

    public bool isPlacingModeOn = false;

    private void Update() {
        if (isHoldingObject && CurrentHeldObject.GetComponent<Crate>().HeldObjectsType == HeldObjectsType.Crate) {

            if (Input.GetKeyDown(KeyCode.Alpha2)) {
                if (isPlacingModeOn == false) {
                    isPlacingModeOn = true;
                } else {
                    isPlacingModeOn = false;
                    Destroy(holdingObjectHologram);
                    isHologramActive = false;
                }
            }

            if (isPlacingModeOn) {

                Ray ray = CameraController.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;


                if (!isHologramActive) {

                    holdingObjectHologram = Instantiate(CurrentHeldObject.GetComponent<Crate>().crateHologram, Vector3.zero, Quaternion.identity);
                    isHologramActive = true;
                }

                if (Physics.Raycast(ray, out hit, 10f, groundLayerMask, QueryTriggerInteraction.UseGlobal)) {

                    holdingObjectHologram.transform.position = hit.point;
                    holdingObjectHologram.transform.rotation = Quaternion.Euler(holdingObjectHologramRotation);
                }

                if (Physics.Raycast(ray, out hit, 5f)) {
                    if (Input.GetKeyDown(KeyCode.R)) {
                        holdingObjectHologramRotation += new Vector3(0, 90, 0);
                    }

                    if (Input.GetKeyDown(KeyCode.E)) {
                        print("Placing item");
                        CurrentHeldObject.transform.position = hit.point;
                        CurrentHeldObject.transform.rotation = Quaternion.Euler(holdingObjectHologramRotation);
                        CurrentHeldObject.transform.parent = null;
                        CurrentHeldObject.AddComponent<Rigidbody>();
                        ResetToDefault();
                    }
                }
            }
        }
    }

    public void AddItemToHand(GameObject newItem) {
        if (hand.childCount == 0) {
            isHoldingObject = true;
            Destroy(newItem.GetComponent<Rigidbody>());
            newItem.transform.parent = hand;
            newItem.transform.position = hand.transform.position;
            newItem.transform.rotation = hand.transform.rotation;
            CurrentHeldObject = newItem;
        }
    }

    public void ResetToDefault() {

        CurrentHeldObject = null;
        isHoldingObject = false;
        Destroy(holdingObjectHologram);
        isHologramActive = false;
    }
}

public enum HeldObjectsType {
    None,
    Crate
}