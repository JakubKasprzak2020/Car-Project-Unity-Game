using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private float speed;
    private float speedForStatic;
    public bool isStatic = true;
    public bool isFrontDynamic = false;
    public bool isVerticalDynamic = false;
    public bool isMovingLeftRight = false;
    private float border = -6;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        checkSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        checkSpeed();
        Move();
        DestroyAfterBorder();
    }

    void Move()
    {
        Vector3 vec = transform.position;
        vec.z -= Time.deltaTime * 20 * speed;
        if(isVerticalDynamic && isMovingLeftRight)
        {
            vec.x += Time.deltaTime * 20 * speed;
        }
        else if(isVerticalDynamic)
        {
            vec.x -= Time.deltaTime * 20 * speed;
        }
        transform.position = vec;
    }

    void DestroyAfterBorder()
    {
        if (isObstacleOnBorder())
        {
            Destroy(gameObject);
        }
    }

    bool isObstacleOnBorder()
    {
        return transform.position.z < border || transform.position.x < border || transform.position.x > -border;
    }

    void checkSpeed()
    {
        speedForStatic = gameManagerScript.Speed;
        if (isStatic || isVerticalDynamic)
        {
            speed = speedForStatic;
        }
        else if (isFrontDynamic)
        {
            speed = speedForStatic * 2;
        }
    }
}
