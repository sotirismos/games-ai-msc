using UnityEngine;

public class ParticleSeekBehavior : MonoBehaviour
{
    public Transform target;

    public float maxAcceleration = 10f;
    public Vector2 velocity = new(1f, 0f); // Movement direction and speed in 2D

    public float duration = 10f; 
    private float elapsedTime = 0f;
    private float fixedY; // Fixed height on the Y-axis

    void Start()
    {
        fixedY = transform.position.y; // Store the initial Y position
    }

    void Update()
    {
        if (elapsedTime < duration)
        {

            Vector3 acceleration = Seek(target.position);

            // Update velocity
            velocity += new Vector2(acceleration.x, acceleration.z) * Time.deltaTime;

            // Update the position based on the velocity
            Vector3 newPosition = transform.position + new Vector3(velocity.x, 0f, velocity.y) * Time.deltaTime;

            // Keep Y position fixed
            transform.position = new Vector3(newPosition.x, fixedY, newPosition.z);

            // TODO: Revisit
            // if (velocity.sqrMagnitude > 0.01f)
            // {
            //     transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.y));
            // }

            // Update elapsed time
            elapsedTime += Time.deltaTime;
        }
    }

    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 direction = new(targetPosition.x - transform.position.x, 0f, targetPosition.z - transform.position.z);

        Vector3 normalizedDirection = direction.normalized;

        Vector3 acceleration = normalizedDirection * maxAcceleration;

        return acceleration;
    }
}