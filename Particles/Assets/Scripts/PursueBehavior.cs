using UnityEngine;

public class PursueBehavior : MonoBehaviour
{
    public Transform target;
    public Vector2 targetVelocity = new(1f, 0f); // Movement direction and speed in 2D

    public float maxAcceleration = 10f;
    public float maxSpeed = 5f;
    public float maxTime = 5f;

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

            Vector3 acceleration = Pursue(transform.position, target.position, targetVelocity, maxTime, maxSpeed);

            // Update velocity
            velocity += new Vector2(acceleration.x, acceleration.z) * Time.deltaTime;

            // Update the position based on the velocity
            Vector3 newPosition = transform.position + new Vector3(velocity.x, 0f, velocity.y) * Time.deltaTime;

            // Keep Y position fixed
            transform.position = new Vector3(newPosition.x, fixedY, newPosition.z);

            // TODO: Revisit
            //if (velocity.sqrMagnitude > 0.01f)
            // {
            //     transform.rotation = Quaternion.LookRotation(new Vector3(velocity.x, 0f, velocity.y));
            // }

            // Update elapsed time
            elapsedTime += Time.deltaTime;

        }
    }

    public Vector3 Pursue(Vector3 characterPosition, Vector3 targetPosition, Vector3 targetVelocity, float maxTime, float maxSpeed)
    {
        Vector3 direction = targetPosition - characterPosition;

        float distance = direction.magnitude;
        float time = distance / maxSpeed;

        if (time > maxTime)
        {
            time = maxTime;
        }

        Vector3 prediction = targetPosition + targetVelocity * time;

        return Seek(prediction);
    }

    private Vector3 Seek(Vector3 targetPosition)
    {
        Vector3 direction = new(targetPosition.x - transform.position.x, 0f, targetPosition.z - transform.position.z);

        Vector3 normalizedDirection = direction.normalized;

        Vector3 acceleration = normalizedDirection * maxAcceleration;

        return acceleration;
    }
}
