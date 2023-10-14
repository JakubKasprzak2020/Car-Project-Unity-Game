using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;
    private SpawnManagerScript spawnManagerScript;
    private PlayerScript playerScript;
    private int level = 1;
    private int levelSize = 5;
    private int maxLevelToIncreaseSpeed = 12; // 15 too hard
    [SerializeField] float speed;
    private float accelerationForEachLevel = 0.015f; //0.025 was too dificult
    private float beginingSpeed = 0.15f;
    private bool bossFightIsOver = false;
    private bool isSlowingDownStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        speed = beginingSpeed;
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLevelAndSpeed();
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
            Debug.Log("You lost");
        } else if (speed == 0 && !playerScript.IsDestroyed)
        {
            Debug.Log("You win");
            playerScript.SetGameIsOver();
        }
    }


}
