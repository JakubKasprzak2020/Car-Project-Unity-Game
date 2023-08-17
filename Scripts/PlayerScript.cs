using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0, 0.5f, 0);
    private Vector3 vec;
    public float speed = 0.6f;
    float border = 4.5f;


    // Start is called before the first frame update
    void Start()
    {
        PutOnStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        CheckBorders();
    }

    void PutOnStartPosition()
    {
        transform.position = startPosition;
    }

    void PlayerMovement()
    {
        vec = transform.position;
        vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20 * speed;
        vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20 * speed;
        transform.position = vec;
    }

    void CheckBorders()
    {
        CheckUpBorder();
        CheckDownBorder();
        CheckLeftBorder();
        CheckRightBorder();
    }
    void CheckUpBorder()
    {
        if (transform.position.z > border)
        {
            Vector3 vec = transform.position;
            vec.z = border;
            transform.position = vec;
        }
    }

    void CheckDownBorder()
    {
        if (transform.position.z < -border)
        {
            Vector3 vec = transform.position;
            vec.z = -border;
            transform.position = vec;
        }
    }

    void CheckLeftBorder()
    {
        if (transform.position.x < -border)
        {
            Vector3 vec = transform.position;
            vec.x = -border;
            transform.position = vec;
        }
    }

    void CheckRightBorder()
    {
        if (transform.position.x > border)
        {
            Vector3 vec = transform.position;
            vec.x = border;
            transform.position = vec;
        }
    }
}
