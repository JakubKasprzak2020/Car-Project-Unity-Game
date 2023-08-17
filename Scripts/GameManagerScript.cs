using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManagerScript : MonoBehaviour
{
    [SerializeField] float speed = 0.15f;
    private float accelerationForEachLevel = 0.025f;
    public GameObject spawnManager;
    private SpawnManagerScript spawnManagerScript;
    private int level = 1;
    private int levelSize = 10;

    // Start is called before the first frame update
    void Start()
    {
        spawnManagerScript = spawnManager.GetComponent<SpawnManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        changeLevelAndSpeed();
    }

    void changeLevelAndSpeed()
    {
        int obstacleCounter = spawnManagerScript.SpawnCounter;
        if (obstacleCounter == level * levelSize)
        {
            level++;
            speed += accelerationForEachLevel;
        }
    }

    public float Speed
    {
        get { return speed; }
    }
}
