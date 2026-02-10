using UnityEngine;

public class WorldRotator : MonoBehaviour
{
    public float rotationSpeed = 50f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");   

        transform.Rotate(Vector3.forward, h * rotationSpeed * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, -v * rotationSpeed * Time.deltaTime, Space.World);
    }
}
