using UnityEngine;

public class ParticleController : MonoBehaviour
{
    public Vector3 velocity;
    public float mass;
    public float dragCoeff;
    public float restitutionCoeff;
    public Vector3 cubeMinPosition;
    public Vector3 cubeMaxPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // empty, no specific setup occurs. 
    }

    private Vector3 GetFacetNormal(int facetIndex)
    {
        return facetIndex switch
        {   // +x, +y, +z, -x, -y, -z
            0 => new Vector3(1f, 0f, 0f),
            1 => new Vector3(0f, 1f, 0f),
            2 => new Vector3(0f, 0f, 1f),
            3 => new Vector3(-1f, 0f, 0f),
            4 => new Vector3(0f, -1f, 0f),
            5 => new Vector3(0f, 0f, -1f),
            _ => throw new System.ArgumentOutOfRangeException("Invalid facet index"),
        };
    }

    // Get the reference point
    private Vector3 GetFacetPoint(int facetIndex)
    {
        if (facetIndex < 3)
        {
            return cubeMinPosition;
        }
        else
        {
            return cubeMaxPosition;
        }
    }

    // ray-plane intersection analysis
    private Vector3 FindCollisionPosition(Vector3 planePoint, Vector3 planeNormal, Vector3 linePoint1, Vector3 linePoint2)
    {
        Vector3 lineVec = linePoint2 - linePoint1;
        float d = Vector3.Dot(planePoint - linePoint1, planeNormal) / Vector3.Dot(lineVec, planeNormal);

        return linePoint1 + lineVec * d;
    }

    private bool CheckForCollisionWithPlane(Vector3 oldPosition, Vector3 newPosition, Vector3 newVelocity, float dt, int facetIndex,
        out Vector3 correctedPosition, out Vector3 correctedVelocity, out Vector3 collisionPosition, out Vector3 collisionVelocity, out float collisionTime)
    {
        Vector3 normal = GetFacetNormal(facetIndex);
        Vector3 pointOnPlane = GetFacetPoint(facetIndex);

        // Check if newPosition is on the correct side of the plane or if the particle is not moving
        if (Vector3.Dot(normal, newPosition - pointOnPlane) >= 0 || Vector3.Distance(newPosition, oldPosition) < float.Epsilon)
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

        // 1) Collision position via ray-plane intersection
        collisionPosition = FindCollisionPosition(pointOnPlane, normal, oldPosition, newPosition);

        // 2) Time of collision
        collisionTime = Vector3.Distance(oldPosition, collisionPosition) / velocity.magnitude;

        // 3) Decompose velocity into normal and tangential components
        Vector3 normalVelocity = Vector3.Dot(velocity, normal) * normal;
        Vector3 tangentVelocity = velocity - normalVelocity;

        // 4) Apply restitution
        Vector3 newNormalVelocity = -restitutionCoeff * normalVelocity;
        collisionVelocity = newNormalVelocity + tangentVelocity;

        // 5) Use leftover time to update position/velocity from collision point (using Euler)
        float remainingTime = dt - collisionTime;
        CalcNewState(collisionPosition, collisionVelocity, remainingTime, out correctedPosition, out correctedVelocity);

        return true;
    }

    // Detecting the earliest collision among the 6 bounding planes
    private bool DetectAndResolveCollision(Vector3 oldPosition, Vector3 newPosition, Vector3 newVelocity, float dt,
        out Vector3 correctedPosition, out Vector3 correctedVelocity, out Vector3 collisionPosition, out Vector3 collisionVelocity, out float collisionTime)
    {
        Vector3 CorrectedPosition = newPosition;
        Vector3 CorrectedVelocity = newVelocity;
        Vector3 CollisionPosition = newPosition;
        Vector3 CollisionVelocity = newVelocity;
        float bestCollisionTime = dt;
        float minDist = Vector3.Distance(oldPosition, newPosition);
        bool ret = false;

        // Check all 6 planes
        for (int i = 0; i < 6; i++)
        {
            if (CheckForCollisionWithPlane(oldPosition, newPosition, newVelocity, dt, i,
                out Vector3 tempCorrectedPosition, out Vector3 tempCorrectedVelocity, out Vector3 tempCollisionPosition, out Vector3 tempCollisionVelocity,
                out float tempCollisionTime))
            {
                float dist = Vector3.Distance(oldPosition, tempCollisionPosition);

                // Pick the "earliest" collision = smallest distance from oldPosition
                if (dist < minDist)
                {
                    minDist = dist;
                    CorrectedPosition = tempCorrectedPosition;
                    CorrectedVelocity = tempCorrectedVelocity;
                    CollisionPosition = tempCollisionPosition;
                    CollisionVelocity = tempCollisionVelocity;
                    bestCollisionTime = tempCollisionTime;
                }

                ret = true;
            }
        }

        correctedPosition = CorrectedPosition;
        correctedVelocity = CorrectedVelocity;
        collisionPosition = CollisionPosition;
        collisionVelocity = CollisionVelocity;
        collisionTime = bestCollisionTime;

        return ret;
    }

    private void ResolveCollisions(Vector3 oldPosition, Vector3 oldVelocity, Vector3 newPosition, Vector3 newVelocity, float dt,
                               out Vector3 correctedPosition, out Vector3 correctedVelocity)
    {
        correctedPosition = newPosition;
        correctedVelocity = newVelocity;

        int cnt = 0;

        while (dt >= 0 && cnt < 100)
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

            cnt++;
        }
    }

    private void CalcNewState(Vector3 oldPosition, Vector3 oldVelocity, float dt, 
                          out Vector3 newPosition, out Vector3 newVelocity)
    {
        Vector3 gravity = new(0f, -9.807f, 0f);
        Vector3 gravityForce = mass * gravity;

        // This is the wind force
        Vector3 dragForce = -dragCoeff * oldVelocity;

        // Total force affecting the particle
        Vector3 totalForce = gravityForce + dragForce;

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

        // 2) Resolve collisions within the bounding cube for the time step dt.
        ResolveCollisions(oldPosition, oldVelocity, newPosition, newVelocity, dt,
            out Vector3 correctedPosition, out Vector3 correctedVelocity);

        // 3) Update transform and velocity with the final post-collision state.
        transform.position = correctedPosition;
        velocity = correctedVelocity;
    }
}