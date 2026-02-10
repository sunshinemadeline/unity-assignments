using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    public Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y < -1f)
        {
            transform.position = startPosition;
            GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        }
    }
}

