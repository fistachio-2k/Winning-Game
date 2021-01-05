using UnityEngine;
using Cinemachine;

public class CinemachinePOVExtention : CinemachineExtension
{
    [SerializeField]
    private float horizontalSpeed = 50f;
    [SerializeField]
    private float verticalSpeed = 50f;
    [SerializeField]
    private float clampAngleTop = 60f;
    [SerializeField]
    private float clampAngleBottom = -40;

    private InputManager inputManager;
    private Vector3 startingRotation;
    protected override void Awake()
    {
        inputManager = InputManager.Instance;
        startingRotation = transform.localRotation.eulerAngles;
        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, clampAngleBottom, clampAngleTop);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, startingRotation.x, 0f);
            }
        }
    }
}
