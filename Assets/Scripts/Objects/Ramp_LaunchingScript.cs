using UnityEngine;

public class Ramp_LaunchingScript : MonoBehaviour
{
    PrometeoCarController carController;
    int originalCarAccel;
    int originalCarSpeed;
    void Start()
    {
        carController = PrometeoCarController.Instance;
        originalCarAccel = carController.accelerationMultiplier;
        originalCarSpeed = carController.maxSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Something entered the trigger: " + other.gameObject.name + " with tag: " + other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Ramping");
            carController.maxSpeed = 300;
            carController.accelerationMultiplier = 10;
            carController.GoForward();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Something exited the trigger: " + other.gameObject.name + " with tag: " + other.gameObject.tag);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Not Ramping");
            carController.maxSpeed = originalCarSpeed;
            carController.accelerationMultiplier = originalCarAccel;
        }
    }
}
