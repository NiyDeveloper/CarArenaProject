using UnityEngine;
using UnityEngine.InputSystem;

public class Car_MovementScript : MonoBehaviour
{
    // Fields
    [SerializeField] float acceleration;
    [SerializeField] float handling;
    [SerializeField] Transform cameraPos;

    // Private Variables
    Rigidbody sphereRB;

    // Public Variables
    public static Vector3 spherePosition;

    private void Start()
    {
        sphereRB = GetComponent<Rigidbody>();
    }
    void Update()
    {
        spherePosition = transform.position;
    }

    void FixedUpdate()
    {
        if (Keyboard.current.wKey.isPressed)
            sphereRB.linearVelocity += new Vector3(0f, 0f, acceleration);
        if (Keyboard.current.aKey.isPressed)
            sphereRB.linearVelocity += new Vector3(-handling, 0f, 0f);
        if (Keyboard.current.sKey.isPressed)
            sphereRB.linearVelocity += new Vector3(0f, 0f, -acceleration);
        if (Keyboard.current.dKey.isPressed)
            sphereRB.linearVelocity += new Vector3(handling, 0f, 0f);
        if (Keyboard.current == null)
            sphereRB.linearVelocity = new Vector3(sphereRB.linearVelocity.x * 0.5f, 0f, sphereRB.linearVelocity.z * 0.5f);
    }
}
