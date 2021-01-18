using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private InputManager inputManager;
    private Transform cameraTransform;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        if (GameEventsManager._instance.currVcam == GameEventsManager.Vcam.Sitting)
        {
            if (inputManager.GetMouseClick())
            {
                return;
            }
        }
        else
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector2 movement = inputManager.GetPlayerMovment();
            Vector3 move = new Vector3(movement.x, 0f, movement.y);
            move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0;
            controller.Move(move * Time.deltaTime * playerSpeed);

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);

        }
    }
}
