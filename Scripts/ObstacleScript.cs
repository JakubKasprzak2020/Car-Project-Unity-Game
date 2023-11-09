using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public GameObject gameManager;
    private GameManagerScript gameManagerScript;
    private float speed;
    private float speedForStatic;
    private float speedBeforeItChangeToZero;
    public bool isStatic = true;
    public bool isFrontDynamic = false;
    public bool isHorizontalDynamic = false;
    public bool isMovingLeftRight = false;
    private float border = -12;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        CheckSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
        Move();
        DestroyAfterBorder();
    }

    void Move()
    {
        Vector3 vec = transform.position;
        vec.z -= Time.deltaTime * 20 * speed;
        if(isHorizontalDynamic && isMovingLeftRight)
        {
            vec.x += Time.deltaTime * 20 * speedBeforeItChangeToZero;
        }
        else if(isHorizontalDynamic)
        {
            vec.x -= Time.deltaTime * 20 * speedBeforeItChangeToZero;
        }
        transform.position = vec;
    }

    void DestroyAfterBorder()
    {
        if (IsObstacleOnBorder())
        {
            Destroy(gameObject);
        }
    }

    bool IsObstacleOnBorder()
    {
        return transform.position.z < border || transform.position.x < border || transform.position.x > -border;
    }

    void CheckSpeed()
    {
        speedForStatic = gameManagerScript.Speed;
        if (isStatic || isHorizontalDynamic)
        {
            speed = speedForStatic;
        }
        else if (isFrontDynamic)
        {
            speed = speedForStatic * 2;
        }
        if (speed != 0)
        {
            speedBeforeItChangeToZero = speed;
        }
    }

}
