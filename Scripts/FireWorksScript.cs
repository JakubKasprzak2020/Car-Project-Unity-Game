using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWorksScript : MonoBehaviour
{
    [SerializeField] ParticleSystem fireworks;
    // Start is called before the first frame update
    void Start()
    {
        fireworks.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RunFireworks()
    {
        fireworks.Play();
    }
}
