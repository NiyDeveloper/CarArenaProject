using UnityEngine;
using UnityEngine.InputSystem;

public class Camera_PostionScript : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] float cameraSens; // Sensitivity
    [SerializeField] float pitchUpperLimit; // Pitch upper rotation limit
    [SerializeField] float pitchLowerLimit; // Pitch lower rotation limit
    [SerializeField] Transform carTransform; // Car transform
    Vector2 mouseVector;
    InputAction lookAction;

    float pitch;
    float yaw;
    float distance;
    float height;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        lookAction = inputActions.FindAction("Look");
        height = transform.localPosition.y;
        distance = transform.localPosition.z;
    }

    void Update()
    {
        mouseVector = lookAction.ReadValue<Vector2>();
    }

    void LateUpdate()
    {
        Quaternion rotation = RotateCamera();
        // Calculate offset direction using pitch and yaw rotation
        Vector3 positionOffset = rotation * new Vector3(0, height, distance);

        // Position camera behind/around the target and apply rotation
        transform.position = carTransform.position + positionOffset;
        transform.rotation = rotation;
    }

    Quaternion RotateCamera()
    {
        pitch += -mouseVector.y * cameraSens;
        pitch = Mathf.Clamp(pitch, pitchLowerLimit, pitchUpperLimit);
        yaw += mouseVector.x * cameraSens;
        return Quaternion.Euler(pitch, yaw, 0);
    }

}