using UnityEngine;
using UnityEngine.InputSystem;

public class Car_MovementScript : MonoBehaviour
{

    // Fields
    [Header("Wheel Colliders")]
    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider rearLeftWheelCollider;
    [SerializeField] WheelCollider rearRightWheelCollider;

    [Header("Wheel Meshes")]
    [SerializeField] Transform frontLeftMesh;
    [SerializeField] Transform frontRightMesh;
    [SerializeField] Transform rearLeftMesh;
    [SerializeField] Transform rearRightMesh;

    [Header("Physics Parameters")]
    [SerializeField] float motorForce = 1000f;       // Acceleration force
    [SerializeField] float brakeForce = 2000f;       // Braking force
    [SerializeField] float maxSteerAngle = 30f;      // Maximum turning angle
    [SerializeField] float steerSpeed = 5f;          // Steering speed
    float currentSteerAngle = 0f;
    float currentMotorForce = 0f;
    float currentBrakeForce = 0f;

    [Header("Imported Tools")]

    [SerializeField] InputActionAsset inputActions;
    InputAction moveAction;
    Vector2 mouseVector;
    float moveInput;
    float steerInput;

    void Start()
    {
        moveAction = inputActions.FindAction("Move");
    }

    void FixedUpdate()
    {
        mouseVector = moveAction.ReadValue<Vector2>();
        moveInput = mouseVector.y;
        steerInput = mouseVector.x;
        // 2. Handle Acceleration & Braking (W/S)
        currentMotorForce = moveInput * motorForce;
        
        // If pressing opposite to movement, apply brakes
        if (moveInput < 0 && Vector3.Dot(transform.forward, GetComponent<Rigidbody>().linearVelocity) > 0.1f)
        {
            currentBrakeForce = brakeForce;
            currentMotorForce = 0f;
        }
        else if (moveInput > 0 && Vector3.Dot(transform.forward, GetComponent<Rigidbody>().linearVelocity) < -0.1f)
        {
            currentBrakeForce = brakeForce;
            currentMotorForce = 0f;
        }
        else
        {
            currentBrakeForce = 0f;
        }

        // Apply forces to rear wheels (Rear-Wheel Drive setup)
        rearLeftWheelCollider.motorTorque = currentMotorForce;
        rearRightWheelCollider.motorTorque = currentMotorForce;
        rearLeftWheelCollider.brakeTorque = currentBrakeForce;
        rearRightWheelCollider.brakeTorque = currentBrakeForce;

        // 3. Slowly Turn Towards Target Direction (Smoothing)
        float targetSteerAngle = steerInput * maxSteerAngle;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, Time.fixedDeltaTime * steerSpeed);

        // Apply steering to front wheels
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;

        // 4. Update Visual Wheel Meshes
        UpdateWheelPosition(frontLeftWheelCollider, frontLeftMesh);
        UpdateWheelPosition(frontRightWheelCollider, frontRightMesh);
        UpdateWheelPosition(rearLeftWheelCollider, rearLeftMesh);
        UpdateWheelPosition(rearRightWheelCollider, rearRightMesh);
    }

    void UpdateWheelPosition(WheelCollider collider, Transform transformMesh)
    {
        if (transformMesh == null) return;

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        transformMesh.position = position;
        transformMesh.rotation = rotation;
    }
}