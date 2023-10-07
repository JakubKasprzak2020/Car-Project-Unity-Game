using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StreetScript : MonoBehaviour
{
    public GameObject gameManager;
    Vector3 beginingPosition = new Vector3(0, 0, 15);
    GameManagerScript gameManagerScript;
    int zVectorBorder = -15;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpeed();
        Move();
        TeleportToBeginingPosition();
    }

    void Move()
    {
        Vector3 vec = transform.position;
        vec.z -= Time.deltaTime * 20 * speed;
        transform.position = vec;
    }

    void TeleportToBeginingPosition()
    {
        Vector3 vec = transform.position;
        if (vec.z <= zVectorBorder)
        {
            transform.position = beginingPosition;
        }
    }

    void CheckSpeed()
    {
        speed = gameManagerScript.Speed;
    }
}
