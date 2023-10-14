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
    public List<GameObject> powerUps;
    public GameObject boss;
    private System.Random rnd = new System.Random();
    private float startDelay = 3; //2 was too fast
    private float repeatRate = 2.2f; //1.5 for abstract prototype, 2.8 bit boring, 2.2 quite chalanging
    private int spawnCounter = 0;
    private int lastLevel = 1;
    private int noPowerUpBeginingLimit = 9; // 20 - try with 9 now
    private int howManyObstaclesForPowerUp = 9; //10  - try with 9 now
    private int endOfSpawiningLimit = 100; //100
    private bool bossWasSpawned = false;

    // Start is called before the first frame update
    void Start()
    {
        obstaclesInPlay = new List<GameObject>();
        obstaclesInPlay.Add(obstacles[0]);
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        InvokeRepeating("Spawn", startDelay, repeatRate);
        //InvokeRepeating("FinalSpawn", startDelay, repeatRate); //for testing final
    }

    // Update is called once per frame
    void Update()
    {
        calculateRepeatRate();
    }

    void Spawn()
    {
        if (gameManagerScript.Speed > 0 && spawnCounter < endOfSpawiningLimit)
        {
            addObstacleOnLevel();
            if (ShouldSpawnPowerUp())
            {
                SpawnPowerUp();
            }
            if (gameManagerScript.Level < 5) //5
            {
                SpawnObstacle();
            }
            else if (gameManagerScript.Level < 7) //7
            {
                SpawnWithVertical();
            }
            else if (gameManagerScript.Level < 15) // 15
            {
                spawnWithVertivalAndLong();
            }
            else
            {
                FinalSpawn();
            }
            spawnCounter++;
            if (spawnCounter > endOfSpawiningLimit)
            {
                spawnCounter = endOfSpawiningLimit;
            }
        }
        if (spawnCounter == endOfSpawiningLimit && !bossWasSpawned)
        {
            bossWasSpawned = true;
            StartCoroutine(SpawnBoss());
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

    bool ShouldSpawnPowerUp()
    {
        return spawnCounter > noPowerUpBeginingLimit && spawnCounter % howManyObstaclesForPowerUp == 0;
    }

    void SpawnPowerUp()
    {
        GameObject powerUp = GetRandomObstacle(powerUps);
        int vectorX = rnd.Next(-3, 3);
        Vector3 spawnPosition = new Vector3(vectorX, 0.5f, 8.5f);
        Instantiate(powerUp, spawnPosition, powerUp.transform.rotation);
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

    void FinalSpawn()
    {
        int randomChoice = rnd.Next(0, 6);

        switch (randomChoice)
        {
            case 0:
                SpawnDiagonal1();
                break;

            case 1:
                SpawnDiagonal2();
                break;
            case 2:
                SpawnLeftRightDynamic();
                break;
            case 3:
                SpawnCenterDynamic();
                break;
            case 4:
                SpawnManyVerticalLeft();
                break;
            case 5:
                SpawnManyVerticalRight();
                break;
        }
    }

    void SpawnDiagonal1()
    {
        GameObject obstacle = obstaclesInPlay[0];
        Vector3 spawnPosition1 = new Vector3(-3, 0, 6);
        Vector3 spawnPosition2 = new Vector3(-2, 0, 7);
        Vector3 spawnPosition3 = new Vector3(-1, 0, 8);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition3, obstacle.transform.rotation);
    }

    void SpawnDiagonal2()
    {
        GameObject obstacle = obstaclesInPlay[0];
        Vector3 spawnPosition1 = new Vector3(3, 0, 6);
        Vector3 spawnPosition2 = new Vector3(2, 0, 7);
        Vector3 spawnPosition3 = new Vector3(1, 0, 8);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition3, obstacle.transform.rotation);
    }

    void SpawnLeftRightDynamic()
    {
        GameObject obstacle = obstacles[3];
        Vector3 spawnPosition1 = new Vector3(2.5f, 0, 6);
        Vector3 spawnPosition2 = new Vector3(-2.5f, 0, 6);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
    }
    
    void SpawnCenterDynamic()
    {
        GameObject obstacle = obstacles[2];
        Vector3 spawnPosition1 = new Vector3(0, 0, 6);
        Vector3 spawnPosition2 = new Vector3(-1.5f, 0, 10);
        Vector3 spawnPosition3 = new Vector3(1.5f, 0, 10);
        //Vector3 spawnPosition4 = new Vector3(0, 0, 11);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition3, obstacle.transform.rotation);
        //Instantiate(obstacle, spawnPosition4, obstacle.transform.rotation);
    }

    void SpawnManyVerticalLeft()
    {
        GameObject obstacle = verticalObstacles[0];
        Vector3 spawnPosition1 = new Vector3(-5, 0, 9);
        Vector3 spawnPosition2 = new Vector3(-8, 0, 9);
        Vector3 spawnPosition3 = new Vector3(-11, 0, 9);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition3, obstacle.transform.rotation);
    }

    void SpawnManyVerticalRight()
    {
        GameObject obstacle = verticalObstacles[1];
        Vector3 spawnPosition1 = new Vector3(5, 0, 9);
        Vector3 spawnPosition2 = new Vector3(8, 0, 9);
        Vector3 spawnPosition3 = new Vector3(11, 0, 9);
        Instantiate(obstacle, spawnPosition1, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition2, obstacle.transform.rotation);
        Instantiate(obstacle, spawnPosition3, obstacle.transform.rotation);
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(10);
        Vector3 spawnPosition = new Vector3(0, 0, 6);
        Instantiate(boss, spawnPosition, boss.transform.rotation);
    }

    public int SpawnCounter
    {
        get { return spawnCounter; }
    }
}
