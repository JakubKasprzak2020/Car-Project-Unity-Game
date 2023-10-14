using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    GameObject gameManager;
    Transform turret;
    Transform explosion;
    public GameObject vehicle;
    private GameManagerScript gameManagerScript;
    private float topToBottomSpeed;
    private float bottomTopSpeed = 1;
    private float bottomBorder = -12;
    private bool isGoingFromTopToBottom = true;
    private bool isGoingFromBottomToTop = false;
    private bool turretRotationHasStarted = false;
    private bool wasVehicleSpawned = false;
    private bool explodedAlready = false;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        gameManagerScript = gameManager.GetComponent<GameManagerScript>();
        topToBottomSpeed = gameManagerScript.Speed;
        turret = transform.GetChild(0);
        explosion = transform.GetChild(5);
    }

    // Update is called once per frame
    void Update()
    {
        GoFromTopToBottom();
        GoFromBottomToTop(); 
        RotateTurret();
        SpawnVehicle();
        DestroyOnCollisionWithVehicle();
    }

    void GoFromTopToBottom()
    {
        if (isGoingFromTopToBottom && gameManagerScript.Speed > 0)
        {
            Vector3 vec = transform.position;
            vec.z -= Time.deltaTime * 20 * topToBottomSpeed;
            transform.position = vec;
        }
        if (transform.position.z < bottomBorder)
        {
            isGoingFromTopToBottom = false;
            isGoingFromBottomToTop = true;
        }
    }

    void GoFromBottomToTop()
    {
        if (isGoingFromBottomToTop && gameManagerScript.Speed > 0)
        {
            Vector3 vec = transform.position;
            vec.z += Time.deltaTime * bottomTopSpeed;
            transform.position = vec;
        }
        if (transform.position.z >= 0)
        {
            isGoingFromBottomToTop = false;
        }
    }

    void RotateTurret()
    {
        if (!isGoingFromTopToBottom && !isGoingFromBottomToTop && (!turretRotationHasStarted || turret.localEulerAngles.y < 359)
            && gameManagerScript.Speed > 0)
        {
            turretRotationHasStarted = true;
            turret.Rotate(0, 1, 0, Space.World);
        }
    }

    void SpawnVehicle()
    {
        if (turret.localEulerAngles.y > 350 && !wasVehicleSpawned)
        {
            Vector3 spawnPosition = new Vector3(0, 0, 7);
            Instantiate(vehicle, spawnPosition, vehicle.transform.rotation);
            wasVehicleSpawned = true;
        }
    }

    void DestroyOnCollisionWithVehicle()
    {
        if (wasVehicleSpawned && !explodedAlready)
        {
            explodedAlready = true;
            explosion.GetComponent<ExplosionScript>().Explode();
            StartCoroutine(DestroyAfterExplosion());
        }
    }

    IEnumerator DestroyAfterExplosion()
    {
        yield return new WaitForSeconds(1);
        gameManagerScript.SetBossFightIsOver();
        Destroy(gameObject);
    }
}
