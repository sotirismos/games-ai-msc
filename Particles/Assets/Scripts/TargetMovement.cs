using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    public Vector2 velocity = new(1f, 0f); // Movement direction and speed in 2D
    public float duration = 10f; // Duration of movement

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
            // Update position in X-Z space
            Vector3 newPosition = transform.position + new Vector3(velocity.x, 0, velocity.y) * Time.deltaTime;
            newPosition.y = fixedY; // Keep Y constant
            transform.position = newPosition;

            elapsedTime += Time.deltaTime;
        }
    }
}

