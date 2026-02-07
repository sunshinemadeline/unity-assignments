using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public float forceStrength = 500f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * forceStrength);
        }
    }
}
