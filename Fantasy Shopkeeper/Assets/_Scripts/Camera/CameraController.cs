using UnityEngine;

public class CameraController : MonoBehaviour {

    public Camera mainCamera;

    public Transform cameraParent;

    //Default Pos
    public Transform cameraDefaultPos;
    public Transform cameraDefaultParent;

    public Quaternion cameraDefaultRotation;
    public Quaternion cameraNewRotation = Quaternion.Euler(55, 0, 0);

    public GameObject inventory;

    public bool isInMainPos = true;
    public bool isTransitioning;

    void Update() {

        if (Input.GetKeyDown(KeyCode.I)) {
            if (isInMainPos) {
                ChangeCameraToPos(inventory.transform);
            } else {
                ReturnToDefaultPos();
            }
        }
    }


    public void ChangeCameraToPos(Transform newCamPos) {
        mainCamera.transform.localPosition = Vector3.Slerp(mainCamera.transform.position, Vector3.zero, 1);
        mainCamera.transform.localRotation = Quaternion.Slerp(mainCamera.transform.rotation, cameraNewRotation, 1);
        mainCamera.transform.SetParent(newCamPos);
        isInMainPos = false;
    }

    public void ReturnToDefaultPos() {
        mainCamera.transform.position = cameraDefaultPos.position;
        mainCamera.transform.SetParent(cameraDefaultParent);
        mainCamera.gameObject.SetActive(true);
        isInMainPos = true;
    }
}
