using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class PlayerController : MonoBehaviour {

    #region Movement vars

    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    public Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 25.0f;


    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;

    #endregion

    public LayerMask interactLayerMask;
    public Interactable focus;

    public GameObject inventory;
    public bool isInventoryOpen;

    public CameraController cameraController;

    void Start() {
        characterController = GetComponent<CharacterController>();


        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    void Update() {
        #region Inputs

        if (Input.GetKeyDown(KeyCode.I)) {
            inventory.SetActive(!inventory.activeSelf);
            canMove = !canMove;
        }

        #endregion

        #region Raycast & Pickup
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 5f, interactLayerMask)) {
            Interactable interactable = hit.collider.GetComponent<Interactable>();
            UIManager.instance.OpenInteractPrompt(interactable.name, interactable.interactKey);

            if (Input.GetKeyDown(KeyCode.E)) {
                if (interactable != null) {
                    SetFocus(interactable);
                }
            }
        } else {
            UIManager.instance.CloseInteractPrompt();
        }

        #endregion

        #region Movement

        if (inventory.activeSelf) return;

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) {
            moveDirection.y = jumpSpeed;
        } else {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded) {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (canMove) {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
        characterController.Move(moveDirection * Time.deltaTime);


        #endregion
    }

    void SetFocus(Interactable newFocus) {
        if (newFocus != focus) {
            if (focus != null) {

                focus.OnDefocused();
            }
        }
        newFocus.OnFocused(transform);
    }

    void RemoveFocus() {
        if (focus != null)
            focus.OnDefocused();
        focus = null;
    }
}