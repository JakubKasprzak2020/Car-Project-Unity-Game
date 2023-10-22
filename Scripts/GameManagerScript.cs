using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;
    public GameObject buttonContinue;
    public GameObject buttonBackToMenu;
    private SpawnManagerScript spawnManagerScript;
    private PlayerScript playerScript;
    private GameInfoScript gameInfoScript;
    List<int> checkPoints = new List<int> {35, 70}; //35, 70
    private int level = 1;
    private int levelOnCheckpoint = 1;
    private int levelSize = 5;
    private int maxLevelToIncreaseSpeed = 12; // 15 too hard
    private int currentCheckpoint = 0;
    [SerializeField] float speed;
    private float accelerationForEachLevel = 0.015f; //0.025 was too dificult
    private float beginingSpeed = 0.15f;
    private float speedOnCheckpoint;
    private bool bossFightIsOver = false;
    private bool isSlowingDownStarted = false;
    private bool hasJustStarted;

    // Start is called before the first frame update
    void Start()
    {
        speed = beginingSpeed;
        speedOnCheckpoint = beginingSpeed;
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
        playerScript = player.GetComponent<PlayerScript>();
        gameInfoScript = GetComponent<GameInfoScript>();
        hasJustStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLevelAndSpeed();
        SetCheckpoint();
        StopGameOnPlayerFailure();
        SlowDown();
        FinishGame();
    }

    void ChangeLevelAndSpeed()
    {
        int obstacleCounter = spawnManagerScript.SpawnCounter;
        if (obstacleCounter == level * levelSize)
        {
            level++;
            AccelerateSpeed();
        }
    }

    void AccelerateSpeed()
    {
        if (level <= maxLevelToIncreaseSpeed)
        {
            speed += accelerationForEachLevel;
        }
    }

    void StopGameOnPlayerFailure()
    {
        if (playerScript.IsDestroyed)
        {
            speed = 0;
        }
    }

    public float Speed
    {
        get { return speed; }
    }

    public int Level
    {
        get { return level; }
    }

    public void SetBossFightIsOver()
    {
        bossFightIsOver = true;
    }

    void SlowDown()
    {
        if (bossFightIsOver && !isSlowingDownStarted)
        {
            isSlowingDownStarted = true;
            InvokeRepeating("ReduceSpeed", 10, 0.3f);
            gameInfoScript.ShowMainInfoWithText("YOU DID IT!");
            StartCoroutine(StartFinalAnimationAfterSeconds(7));
        }
    }

    void ReduceSpeed()
    {
        speed -= accelerationForEachLevel;
        if (speed < 0)
        {
            speed = 0;
        }
    }

    void FinishGame()
    {
        if (speed == 0 && playerScript.IsDestroyed)
        {
            buttonContinue.SetActive(true);
            buttonBackToMenu.SetActive(true);
        } else if (speed == 0 && !playerScript.IsDestroyed)
        {
            //Debug.Log("You win");
            //playerScript.SetGameIsOver();
        }
    }

    public void ContinueGame()
    {
        buttonContinue.SetActive(false);
        buttonBackToMenu.SetActive(false);
        playerScript.PutOnStartPosition();
        speed = speedOnCheckpoint;
        level = levelOnCheckpoint;
        spawnManagerScript.ContinueWithSpawnCounterAndLastLevel(currentCheckpoint, levelOnCheckpoint);
        DestroyAllObstaclesAndPowerUps();
        hasJustStarted = true;
    }

    private void DestroyAllObstaclesAndPowerUps()
    {
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        GameObject[] powerUps = GameObject.FindGameObjectsWithTag("Extra Life");
       foreach(GameObject obstacle in obstacles)
       {
           Destroy(obstacle);
       }
       foreach (GameObject powerUp in powerUps)
       {
           Destroy(powerUp);
       }
    }

    private void SetCheckpoint()
    {
        foreach (int checkPoint in checkPoints) {
            if (checkPoint == spawnManagerScript.SpawnCounter && !hasJustStarted)
            {
                levelOnCheckpoint = level;
                speedOnCheckpoint = speed;
                currentCheckpoint = checkPoint;
                gameInfoScript.ShowMainInfoWithText("CHECKPOINT!");
            }
        }
    }

    public void SetHasJustStartedToFalse()
    {
        hasJustStarted = false;
    }

    IEnumerator StartFinalAnimationAfterSeconds(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Menu");
    }

}
