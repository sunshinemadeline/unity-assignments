using UnityEngine;

public class CollisionLogger : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(
            gameObject.name + " collided with " +
            collision.gameObject.name +
            " | Relative Velocity: " +
            collision.relativeVelocity
        );
    }
}
