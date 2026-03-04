using UnityEngine;

public class RaycastController : MonoBehaviour
{
    public Camera mainCamera;
    public SimplePickupSystem pickupSystem;

    public float rayDistance = 50f;
    public LayerMask pickupLayer;          // Only pickup these
    public float holdHeight = 1.5f;        // Held object goes above the hit point

    public KeyCode pickupKey = KeyCode.E;
    public KeyCode dropKey = KeyCode.Q;

    public float moveSpeed = 6f;

    private Vector3 lastHitPoint;

    void Start()
    {
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        // 1) Ray from mouse into world
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Debug ray (yellow line)
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.yellow);

        // 2) Find hit point for placement (hit anything)
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            lastHitPoint = hit.point;

            // Aim the player at the hit point (top-down rotate on Y)
            Vector3 lookDir = lastHitPoint - transform.position;
            lookDir.y = 0f;
            if (lookDir.sqrMagnitude > 0.001f)
                transform.rotation = Quaternion.LookRotation(lookDir);
        }

        // 3) Move held object every frame (above hit point)
        if (pickupSystem != null)
        {
            pickupSystem.UpdatePickupPosition(lastHitPoint + Vector3.up * holdHeight);
        }

        // 4) Pick up (only if ray hits pickup layer)
        if (Input.GetKeyDown(pickupKey))
        {
            RaycastHit pickupHit;
            if (Physics.Raycast(ray, out pickupHit, rayDistance, pickupLayer))
            {
                pickupSystem.Pickup(pickupHit.collider.gameObject);
            }
        }

        // 5) Drop
        if (Input.GetKeyDown(dropKey))
        {
            pickupSystem.Drop();
        }
    }

    void FixedUpdate()
    {
        // Super basic WASD movement on XZ plane
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(h, 0f, v).normalized * moveSpeed * Time.fixedDeltaTime;
        transform.position += move;
    }
}