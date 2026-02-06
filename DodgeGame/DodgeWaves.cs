using UnityEngine;

public class DodgeGameWaves : MonoBehaviour
{
    //Player
    private Vector3 direction;
    public float speed = 5f;
    public float threshold = .7f;
    public float minX = -4.5f;
    public float maxX = 4.5f;
    
    //Obstacles
    public float baseFallSpeed = 3f;
    private float currentFallSpeed;
    public float spawnInterval = 0.5f;
    private float spawnTimer;
    public float fixedY = 8f;
    public float fixedZ = 8f;

    //Waves
    public float waveDuration = 15f;
    public float intermissionDuration = 3f;
    private float waveTimer;
    private bool inIntermission = false;
    private int wave = 1;

    //Score /State
    private float score = 0f;
    private float highScore = 0f;
    private bool isGameOver = false;
    void Start()
    {
        ResetGame();
        Debug.Log("Game Begin! Wave 1 Starts Now!");
    }

    void Update()
    {
        if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
                ResetGame();
            return;
        }

        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            SpawnDodgeBall();
            spawnTimer = 0f;
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
        UpdateScore();
        HandleWaves();
    }

    void Move()
    {
        Vector3 newPosition = transform.position + direction * speed * Time.deltaTime;

        newPosition.x = Mathf.Clamp(newPosition.x, -4.75f, 4.75f);

        transform.position = newPosition;
    }

    void SpawnDodgeBall()
    {
        float randomX = Random.Range(-4.75f, 4.75f);
        Vector3 spawnPosition = new Vector3(randomX, fixedY, fixedZ);
        
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.tag = "Sphere";
        sphere.transform.position = spawnPosition;

        float scale = Random.Range(0.5f, 1.5f);
        sphere.transform.localScale = Vector3.one * scale;

        ObstacleData data = sphere.AddComponent<ObstacleData>();
        data.isSlider = (wave >= 2 && wave % 2 == 0);
    }

    void MoveSpheres()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject sphere in spheres)
        {
            ObstacleData data = sphere.GetComponent<ObstacleData>();

            Vector3 pos = sphere.transform.position;
            pos.y -= baseFallSpeed * Time.deltaTime;

            if (data.isSlider)
            {
                data.timeAlive += Time.deltaTime;
                pos.x += Mathf.Sin(data.timeAlive * 3f) * Time.deltaTime * 2f;
            }

            sphere.transform.position = pos;

            if (pos.y < -6f)
                Destroy(sphere);
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
                GameOver();
            }
        }
    }

     void HandleWaves()
    {
        waveTimer += Time.deltaTime;

        if (!inIntermission && waveTimer >= waveDuration)
        {
            inIntermission = true;
            waveTimer = 0f;
            Debug.Log("Wave " + wave + " complete!");
        }

        if (inIntermission && waveTimer >= intermissionDuration)
        {
            wave++;
            currentFallSpeed += 1f;
            spawnInterval = Mathf.Max(0.15f, spawnInterval - 0.05f);
            waveTimer = 0f;
            inIntermission = false;

            Debug.Log("Wave " + wave + " started!");
        }
    }

    void UpdateScore()
    {
        score += Time.deltaTime;
    }

    void GameOver()
    {
        isGameOver = true;
        highScore = Mathf.Max(highScore, score);
        Debug.Log("GAME OVER! Score: " + Mathf.FloorToInt(score));
        Debug.Log("High Score: " + Mathf.FloorToInt(highScore));
    }

    void ResetGame()
    {
        GameObject[] spheres = GameObject.FindGameObjectsWithTag("Sphere");
        foreach (GameObject s in spheres)
            Destroy(s);

        score = 0f;
        wave = 1;
        waveTimer = 0f;
        spawnTimer = 0f;
        currentFallSpeed = baseFallSpeed;
        spawnInterval = 0.5f;
        inIntermission = false;
        isGameOver = false;

        transform.position = new Vector3(0f, 2f, 8f);
    }

}
