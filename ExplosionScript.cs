using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionParticle;
    [SerializeField] GameObject fire;
    void Start()
    {
        explosionParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Explode()
    {
        explosionParticle.Play();
    }

    public void Burn()
    {
        fire.SetActive(true);
    }
}
