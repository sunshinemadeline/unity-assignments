using UnityEngine;

public class DodgeGame : MonoBehaviour
{
    private Vector3 direction;
    public float speed = 5f;
    public float threshold = 1.0f;
    public float spawnInterval = 0.25f;
    private float timer = 0f;

    public float minX = -3f;
    public float maxX = 3f;
    public float fixedY = 5f;
    public float fixedZ = 8f;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnDodgeBall();
            timer = 0f;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            direction = Vector3.left;
            Move();
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction = Vector3.right;
            Move();
        }
        MoveSpheres();
        CheckNearby();
    }

    void Move()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void SpawnDodgeBall()
    {
        float randomX = Random.Range(minX, maxX);
        Vector3 spawnPosition = new Vector3(randomX, fixedY, fixedZ);
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.tag = "Sphere";
        sphere.transform.position = spawnPosition;
    }

    void MoveSpheres()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphere in spheres)
        {
            sphere.transform.position += Vector3.down * speed * Time.deltaTime;
        }
    }

    void CheckNearby()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphere in spheres)
        {
            if (sphere == gameObject) continue;

            float distance = Vector3.Distance(transform.position, sphere.transform.position);
            if (distance < threshold)
            {

                Debug.Log("Game Over!");
                Application.Quit();
            }
        }
    }
}
