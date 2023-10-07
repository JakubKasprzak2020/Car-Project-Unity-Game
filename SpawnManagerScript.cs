using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject gameManager;
    private GameManagerScript gameManagerScript;
    public List<GameObject> obstacles;
    public List<GameObject> obstaclesInPlay;
    public List<GameObject> verticalObstacles;
    public List<GameObject> longObstacles;
    private System.Random rnd = new System.Random();
    private float startDelay = 3; //2 was too fast
    private float repeatRate = 2.2f; //1.5 for abstract prototype, 2.8 too boring
    private int spawnCounter = 0;
    private int lastLevel = 1;

    // Start is called before the first frame update
    void Start()
    {
        obstaclesInPlay = new List<GameObject>();
        obstaclesInPlay.Add(obstacles[0]);
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        calculateRepeatRate();
    }

    void Spawn()
    {
        if (gameManagerScript.Speed > 0)
        {
            addObstacleOnLevel();
            if (gameManagerScript.Level < 5) //5
            {
                SpawnObstacle();
            }
            else if (gameManagerScript.Level < 7) //7
            {
                SpawnWithVertical();
            }
            else
            {
                spawnWithVertivalAndLong();
            }
            spawnCounter++;
        }
    }

    void spawnWithVertivalAndLong()
    {
        int randomInt = rnd.Next(5);
        if (randomInt <= 3)
        {
            SpawnWithVertical();
        }
        else
        {
            SpawnLongObstacle();
        }
    }

    void SpawnWithVertical()
    {
        int randomInt = rnd.Next(4);
        if (randomInt <= 2)
        {
            SpawnObstacle();
        }
        else
        {
            SpawnVerticalObstacle();
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = GetRandomObstacle(obstaclesInPlay);
        int vectorX = rnd.Next(-3, 3);
        Vector3 spawnPosition = new Vector3(vectorX, 0, 6);
        Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);
    }

    void SpawnVerticalObstacle()
    {
        GameObject obstacle = GetRandomObstacle(verticalObstacles);
        bool isLeftRight = obstacle.GetComponent<ObstacleScript>().isMovingLeftRight;
        int vectorX = 5;
        if (isLeftRight)
        {
            vectorX = -vectorX;
        }
        int vectorZ = rnd.Next(3, 6);
        Vector3 spawnPosition = new Vector3(vectorX, 0, vectorZ);
        Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);
    }

    void SpawnLongObstacle()
    {
        GameObject obstacle = GetRandomObstacle(longObstacles);
        Vector3 spawnPosition = new Vector3(obstacle.transform.position.x, 0, 6);
        Instantiate(obstacle, spawnPosition, obstacle.transform.rotation);

    }

    GameObject GetRandomObstacle(List<GameObject> objects)
    {
        int obstacleIndex = rnd.Next(objects.Count);
        return objects[obstacleIndex];
    }

    void addObstacleOnLevel()
    {
        int level = gameManagerScript.Level;
        if (level > lastLevel && obstacles.Count >= level)
        {
            obstaclesInPlay.Add(obstacles[level-1]);
            accelerateSpawning();

            lastLevel++;

        }
    }

    void calculateRepeatRate()
    {
        //float factorForOneAndHalfSecondAtStart = 0.225f;
        //float factorFor2point8SecondsAtStart = 0.42f;
        float factorFor2point2SecondsAtStart = 0.33f;
        repeatRate = factorFor2point2SecondsAtStart / gameManagerScript.Speed;
    }

    void accelerateSpawning()
    {
        calculateRepeatRate();
        CancelInvoke();
        InvokeRepeating("Spawn", repeatRate, repeatRate);
    }

    public int SpawnCounter
    {
        get { return spawnCounter; }
    }
}
