using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject spawnManager;
    private SpawnManagerScript spawnManagerScript;
    private int level = 1;
    private int levelSize = 5;
    [SerializeField] float speed;
    private float accelerationForEachLevel = 0.015f; //0.025 was too dificult
    private float beginingSpeed = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        speed = beginingSpeed;
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ChangeLevelAndSpeed();
        StopGameOnPlayerFailure();
    }

    void ChangeLevelAndSpeed()
    {
        int obstacleCounter = spawnManagerScript.SpawnCounter;
        if (obstacleCounter == level * levelSize)
        {
            level++;
            speed += accelerationForEachLevel;
        }
    }

    void StopGameOnPlayerFailure()
    {
        if (!player.activeInHierarchy)
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

}
