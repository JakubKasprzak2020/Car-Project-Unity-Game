using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Vector3 startPosition = new Vector3(0, 0.5f, -4);
    private Vector3 vec;
    public float speed = 0.6f;
    private int lifes = 5;
    private int lifesOnLastUpdate = 5;
    float border = 4.5f;
    float recoveryTime = 1;
    private bool isImmortal = false;
    private bool isInRecoveryTime = false;
    private bool isDestroyed = false;


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
        ProtectFromLoosingMoreThanOneLifeAtTime();
        DestroyWhenNoLifes();
        OverturnIfDestroyed();
    }

    void PutOnStartPosition()
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
    }

    void PlayerMovement()
    {
        if (!isDestroyed)
        {
            vec = transform.position;
            vec.x += Input.GetAxis("Horizontal") * Time.deltaTime * 20 * speed;
            vec.z += Input.GetAxis("Vertical") * Time.deltaTime * 20 * speed;
            transform.position = vec;
            ChangeImmortalityForTesting();
        }
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

    void ChangeImmortalityForTesting()
    {
        if (Input.GetKeyDown("space"))
        {
            isImmortal = !isImmortal;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Extra Life")
        {
            lifes++;
            RunFireworks();
            Destroy(other.gameObject);
        }
        else if (!isInRecoveryTime)
        {
            lifes--;
            if (lifes < 0)
            {
                lifes = 0;
            }
            Explode();
            StartCoroutine(StartRecoveryTime());
        }
    }

    private void DestroyWhenNoLifes()
    {
        if (!isImmortal && lifes < 1)
        {
            //gameObject.SetActive(false);
            isDestroyed = true;
        }
    }

    private void ProtectFromLoosingMoreThanOneLifeAtTime()
    {
        if (lifesOnLastUpdate > lifes)
        {
            lifes = lifesOnLastUpdate - 1;
        }
        lifesOnLastUpdate = lifes;
    }

    public bool IsImmortal
    {
        get { return isImmortal; }
    }

    private void Explode()
    {
        transform.GetChild(4).GetComponent<ExplosionScript>().Explode();
    }

    private void RunFireworks()
    {
        transform.GetChild(5).GetComponent<FireWorksScript>().RunFireworks();
    }

    private void Burn()
    {
        transform.GetChild(4).GetComponent<ExplosionScript>().Burn();
    }

    public int Lifes
    {
        get { return lifes; }
    }

    public bool IsDestroyed
    {
        get { return isDestroyed; }
    }

    IEnumerator StartRecoveryTime()
    { 
        isInRecoveryTime = true;
        yield return new WaitForSeconds(recoveryTime);
        isInRecoveryTime = false;
    }

    private void OverturnIfDestroyed()
    {
        if (isDestroyed && transform.localEulerAngles.x < 180)
        {
            transform.Rotate(5.0f, 0.0f, 0.0f, Space.World);
        } else if (isDestroyed)
        {
            Burn();
        }
    }
}
