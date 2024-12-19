using UnityEngine;

public class MovingFurnitureSystem : MonoBehaviour {
    public GameObject hologram;
    public GameObject currentlyHeldFurniture;

    public Vector3 hologramRotation = Vector3.zero;

    public LayerMask FurnitureLayerMask;
    public LayerMask GroundLayerMask;

    public Material redHologramMaterial;
    public Material greenHologramMaterial;

    //Picking Up:
    float counter = 0;
    public bool isHoldingFurniture = false;
    public bool canPlace = true;

    // Update is called once per frame
    void Update() {

        if (isHoldingFurniture) {
            Collider[] contacts = Physics.OverlapBox(hologram.transform.position, hologram.GetComponent<BoxCollider>().size / 2, hologram.transform.rotation, FurnitureLayerMask);
            if (contacts.Length > 0) {
                foreach (Collider c in contacts) {
                    if (c.CompareTag("ShelfContainer")) {
                        canPlace = false;
                        hologram.GetComponentInChildren<MeshRenderer>().material = redHologramMaterial;
                    }
                }
            } else {
                canPlace = true;
                hologram.GetComponentInChildren<MeshRenderer>().material = greenHologramMaterial;
            }
        }

        Ray ray = CameraController.instance.mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 3, FurnitureLayerMask) && !isHoldingFurniture) {

            if (hit.collider != null && hit.collider.CompareTag("ShelfContainer")) {
                if (Input.GetKey(KeyCode.F)) {
                    counter += Time.deltaTime;
                    if (counter >= 1.5) {
                        PickUpFurniture(hit.collider.GetComponent<ShelfsContainer>());
                    }
                } else {
                    counter = 0;
                }
            }
        }

        if (Physics.Raycast(ray, out hit, 10f, GroundLayerMask) && isHoldingFurniture) {
            hologram.transform.position = hit.point;
            hologram.transform.rotation = Quaternion.Euler(0, hologramRotation.y, 0);
        }

        if (isHoldingFurniture && Input.GetKeyDown(KeyCode.R)) {
            print("Rotating");
            hologramRotation += new Vector3(0, 90, 0);
            hologram.transform.rotation = Quaternion.Euler(0, hologramRotation.y, 0);
        }

        if (Physics.Raycast(ray, out hit, 10f, GroundLayerMask) && isHoldingFurniture && Input.GetKeyDown(KeyCode.E) && canPlace) {
            currentlyHeldFurniture.gameObject.transform.position = hit.point;
            currentlyHeldFurniture.gameObject.transform.rotation = Quaternion.Euler(0, hologramRotation.y, 0);
            PlaceFurniture();


        }



    }

    public void PickUpFurniture(ShelfsContainer pickedUpShelfContainer) {
        print("Picking Up Shelf");
        hologram = Instantiate(pickedUpShelfContainer.hologram, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        currentlyHeldFurniture = pickedUpShelfContainer.gameObject;
        pickedUpShelfContainer.gameObject.transform.position = Vector3.up * 100;
        isHoldingFurniture = true;
    }

    public void PlaceFurniture() {
        Destroy(hologram);
        hologram = null;
        isHoldingFurniture = false;
        canPlace = true;
    }


}
