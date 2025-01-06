using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    public float lifetime = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (lifetime <= 0)
        {
            Debug.LogWarning("Lifetime must be greater than 0. Defaulting to 5 seconds.");
            lifetime = 5f;
        }
        Destroy(gameObject, lifetime);
    }
}