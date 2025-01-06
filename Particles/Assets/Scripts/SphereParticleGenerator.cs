using System.Collections.Generic;
using UnityEngine;

public class SphereParticleGenerator : MonoBehaviour
{
    // TODO: Revisit
    public static SphereParticleGenerator instance;

    public GameObject particle;
    public int numParticles;
    public List<GameObject> particles;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // TODO: Revisit
        instance = this;

        for (int i = 0; i < numParticles; i++)
        {
            GameObject clone = Instantiate(particle);

            particles.Add(clone);
        }
    }

    // Update is called once per frame (Empty for our case)
    void Update()
    {

    }
}
