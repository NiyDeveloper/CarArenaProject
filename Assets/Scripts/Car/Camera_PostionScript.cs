using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_PostionScript : MonoBehaviour
{
    [SerializeField] InputActionAsset inputActions;
    [SerializeField] float cameraSens;
    [SerializeField] float pitchLimit;
    Vector3 cameraOffset;
    Vector2 mouseVector;
    InputAction lookAction;

    float pitch = 0;
    float yaw = 0;
    void Start()
    {
        cameraOffset = transform.localPosition;
        lookAction = inputActions.FindAction("Look");
    }

    void Update()
    {
        mouseVector = lookAction.ReadValue<Vector2>();
        RotateCamera();
    }
    void LateUpdate()
    {
        transform.position = Car_MovementScript.spherePosition + cameraOffset;
    }

    void RotateCamera()
    {
        pitch += mouseVector.y * cameraSens;
        pitch = Mathf.Clamp(pitch, -pitchLimit, pitchLimit);
        yaw += mouseVector.x * cameraSens;
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

    }

}
