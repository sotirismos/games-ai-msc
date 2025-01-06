using UnityEngine;

public class ArriveBehavior : MonoBehaviour
{
    public Transform target;
    public float targetRadius = 1.0f;
    public float slowRadius = 5.0f;

    public float maxSpeed = 10.0f;
    public float maxAcceleration = 5.0f;

    public Vector3 velocity = Vector3.zero; // The particle is initially at rest

    public float duration = 10f;
    private float elapsedTime = 0.0f;
    private float fixedY; // Fixed height on the Y-axis

    void Start()
    {
        fixedY = transform.position.y; // Store the initial Y position
    }

    void Update()
    {
        if (elapsedTime < duration)
        {
            Vector3 acceleration = Arrive(target.position, velocity, slowRadius, targetRadius, maxAcceleration);

            // Update velocity
            velocity += acceleration * Time.deltaTime;

            // Update the position based on the velocity
            Vector3 newPosition = transform.position + velocity * Time.deltaTime;

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

    private Vector3 Arrive(Vector3 targetPosition, Vector3 characterVelocity, float slowRadius, float targetRadius, float maxAcceleration)
    {
        Vector3 direction = new(targetPosition.x - transform.position.x, 0f, targetPosition.z - transform.position.z);

        float distance = direction.magnitude;

        if (distance < targetRadius)
        {
            return Vector3.zero;
        }
                   
        float targetSpeed = (distance > slowRadius) ? maxSpeed : maxSpeed * (distance / slowRadius);

        Vector3 desiredVelocity = direction.normalized * targetSpeed;

        Vector3 acceleration = (desiredVelocity - characterVelocity) / Time.deltaTime;

        if (acceleration.magnitude > maxAcceleration)
        {
            acceleration = acceleration.normalized * maxAcceleration;
        }

        return acceleration;
    }
}
