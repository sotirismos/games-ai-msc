using UnityEngine;

public class ParticleGenerator : MonoBehaviour
{
    public GameObject particle;
    public float minVelocity;
    public float maxVelocity;
    public float spawnInterval = 1f; // Interval in seconds between particle spawns
    private float timer = 0f; // Timer to track time

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (minVelocity > maxVelocity)
        {
            Debug.LogError("minVelocity must be less than maxVelocity!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnParticle();
            timer = 0f;
        }
    }

    void SpawnParticle()
    {
        GameObject newParticle = Instantiate(particle);

        newParticle.GetComponent<ParticleController>().velocity = new Vector3(
            Random.Range(minVelocity, maxVelocity),
            Random.Range(minVelocity, maxVelocity),
            Random.Range(minVelocity, maxVelocity)
        );
    }
}
