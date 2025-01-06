using UnityEngine;


public class SphereParticleController : MonoBehaviour
{
    public Vector3 velocity;
    public float mass;
    public float forceCoeff;
    public float restitutionCoeff;
    public Vector3 sphereCenter;
    public float sphereRadius;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // empty, no specific setup occurs. 
    }

    // TODO: REVISIT
    private Vector3 FindCollisionPosition(Vector3 linePoint1, Vector3 linePoint2)
    {
        Vector3 lineOrigin = linePoint1;
        Vector3 lineUniVec = (linePoint2 - linePoint1).normalized;

        float dotProduct = Vector3.Dot(lineUniVec, lineOrigin - sphereCenter);
        float discriminant = dotProduct * dotProduct - Vector3.SqrMagnitude(lineOrigin - sphereCenter) + sphereRadius * sphereRadius;

        if (discriminant < 0f) discriminant = 0f;

        float d1 = -dotProduct + Mathf.Sqrt(discriminant);
        float d2 = -dotProduct - Mathf.Sqrt(discriminant);
        Vector3 point1 = lineOrigin + d1 * lineUniVec;
        Vector3 point2 = lineOrigin + d2 * lineUniVec;

        if (Vector3.Distance(linePoint2, point1) < Vector3.Distance(linePoint2, point2))
        {
            return point1;
        }
        else
        {
            return point2;
        }
    }

    private bool DetectAndResolveCollision(Vector3 oldPosition,  Vector3 newPosition, Vector3 newVelocity, float dt,
        out Vector3 correctedPosition, out Vector3 correctedVelocity, out Vector3 collisionPosition, out Vector3 collisionVelocity, out float collisionTime)
    {
        // Check if newPosition is inside the sphere or if the particle is not moving
        if (Vector3.Distance(newPosition, sphereCenter) <= sphereRadius || Vector3.Distance(newPosition, oldPosition) < float.Epsilon)
        {
            // No collision
            correctedPosition = newPosition;
            correctedVelocity = newVelocity;
            collisionPosition = newPosition;
            collisionVelocity = newVelocity;
            collisionTime = dt;
            return false;
        }

        // There's a potential collision. Compute collision details.
        Vector3 velocity = (newPosition - oldPosition) / dt;

        // 1) Collision position (REVISIT HOW IT IS CALCULATED)
        collisionPosition = FindCollisionPosition(oldPosition, newPosition);

        // 2) Time of collision
        collisionTime = Vector3.Distance(oldPosition, collisionPosition) / velocity.magnitude;

        // 3) Decompose velocity into normal and tangential components
        Vector3 normal = (sphereCenter - collisionPosition).normalized;
        Vector3 normalVelocity = Vector3.Dot(velocity, normal) * normal;
        Vector3 tangentVelocity = velocity - normalVelocity;

        // 4) Apply restitution
        Vector3 newNormalVelocity = -restitutionCoeff * normalVelocity;
        collisionVelocity = newNormalVelocity + tangentVelocity;

        // 5) Use leftover time to update position/velocity from collision point (using Euler)
        float remainingTime = dt - collisionTime;
        CalcNewState(collisionPosition, collisionVelocity, remainingTime, out correctedPosition, out correctedVelocity);

        return Vector3.Distance(newPosition, sphereCenter) > sphereRadius;
    }

    private void ResolveCollisions(Vector3 oldPosition, Vector3 oldVelocity, Vector3 newPosition, Vector3 newVelocity, float dt,
        out Vector3 correctedPosition, out Vector3 correctedVelocity)
    {
        correctedPosition = newPosition;
        correctedVelocity = newVelocity;

        while (dt >= 0)
        {
          
            if (DetectAndResolveCollision(oldPosition, newPosition, newVelocity, dt,
                out correctedPosition, out correctedVelocity, out Vector3 collisionPosition, out Vector3 collisionVelocity, out float collisionTime))
            {
                // Update old/new for next iteration
                oldPosition = collisionPosition;
                oldVelocity = collisionVelocity;
                newPosition = correctedPosition;
                newVelocity = correctedVelocity;

                // Subtract collisionTime from the step
                dt -= collisionTime;
            }
            else
            {
                // No more collisions found
                break;
            }
        }
    }

    private void CalcNewState(Vector3 oldPosition, Vector3 oldVelocity, float dt, out Vector3 newPosition, out Vector3 newVelocity)
    {
        Vector3 totalForce = new(0f, 0f, 0f);
        // TODO: Revisit (avoid division by zero)
        float eps = 1e-6f;

        foreach (GameObject otherParticle in SphereParticleGenerator.instance.particles)
        {
            // TODO: Revisit (skip applying force from the particle to itself (self-interaction is not needed).)
            if (otherParticle == this) continue;

            Vector3 distance = transform.position - otherParticle.transform.position;
            if (distance.magnitude < eps)
            {
                distance = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
            Vector3 force = forceCoeff * distance / (distance.magnitude * distance.magnitude * distance.magnitude + eps);

            totalForce += force;
        }

        // Calculate acceleration
        Vector3 acceleration = totalForce / mass;

        // Update position and velocity (Euler method)
        newPosition = oldPosition + oldVelocity * dt;
        newVelocity = oldVelocity + acceleration * dt;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
        Vector3 oldPosition = transform.position;
        Vector3 oldVelocity = velocity;

       
        // 1) Integrate motion over dt (no collision yet).
        CalcNewState(oldPosition, oldVelocity, dt, out Vector3 newPosition, out Vector3 newVelocity);

        // 2) Resolve collisions within the sphere for the time step dt.
        ResolveCollisions(oldPosition, oldVelocity, newPosition, newVelocity, dt,
            out Vector3 correctedPosition, out Vector3 correctedVelocity);

        // 3) Update transform and velocity with the final post-collision state.
        transform.position = correctedPosition;
        velocity = correctedVelocity;
    }
}
