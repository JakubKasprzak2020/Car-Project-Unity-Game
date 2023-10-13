using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeScript : MonoBehaviour
{
    public GameObject ring;
    float ringRotationSpeed = 90f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rotateRing();
    }

    private void rotateRing()
    {
        //ring.transform.Rotate(0.0f, 4.0f, 0.0f, Space.World);
        ring.transform.Rotate(0.0f, ringRotationSpeed * Time.deltaTime, 0.0f, Space.World);
       // ring.transform.Rotate(Vector3.up * Time.deltaTime * 4, Space.World);
    }
}
