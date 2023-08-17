using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerScript : MonoBehaviour
{
    public GameObject gameManager;
    public List<GameObject> obstacles;
    public List<GameObject> verticalObstacles;
    private System.Random rnd = new System.Random();
    private float startDelay = 2;
    private float repeatRate = 1.5f;
    private int spawnCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Spawn", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn()
    {
        int randomInt = rnd.Next(4);
        if (randomInt <= 2)
        {
            SpawnObstacle();
        } else
        {
            SpawnVerticalObstacle();
        }
        spawnCounter++;
    }

    void SpawnObstacle()
    {
        GameObject obstacle = GetRandomObstacle(obstacles);
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

    GameObject GetRandomObstacle(List<GameObject> objects)
    {
        int obstacleIndex = rnd.Next(objects.Count);
        return objects[obstacleIndex];
    }

    public int SpawnCounter
    {
        get { return spawnCounter; }
    }
}
