using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduCollisionDetection : MonoBehaviour
{
    public eduWallCollider[] walls;
    public eduRigidBody[] rigidBodies;
    eduCircleCollider circleA, circleB;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        walls = FindObjectsByType<eduWallCollider>(FindObjectsSortMode.None);
        rigidBodies = FindObjectsByType<eduRigidBody>(FindObjectsSortMode.None);

        foreach (eduRigidBody body in rigidBodies)
        {
            foreach (eduWallCollider wall in walls)
            {
                CircleWallCollision(body, wall);
            }
            foreach (eduRigidBody otherBody in rigidBodies)
            {
                if (body != otherBody)
                    CircleCircleCollision(body, otherBody);
            }
        }
    }

    void CircleWallCollision(eduRigidBody body, eduWallCollider wall)
    {
        if (!body.TryGetComponent<eduCircleCollider>(out var circle)) return;

        Vector2 normal = Vector2.zero;
        Vector2 contactPoint = Vector2.zero;
        bool isColliding = false;

        switch (wall.side)
        {
            case eduWallCollider.Wallside.Left:
                if (body.transform.position.x - circle.radius < wall.line.GetPosition(0).x)
                {
                    normal = Vector2.right;
                    contactPoint = new Vector2(wall.line.GetPosition(0).x, body.transform.position.y);
                    isColliding = true;
                }
                break;
            case eduWallCollider.Wallside.Right:
                if (body.transform.position.x + circle.radius > wall.line.GetPosition(0).x)
                {
                    normal = Vector2.left;
                    contactPoint = new Vector2(wall.line.GetPosition(0).x, body.transform.position.y);
                    isColliding = true;
                }
                break;
            case eduWallCollider.Wallside.Bottom:
                if (body.transform.position.y - circle.radius < wall.line.GetPosition(0).y)
                {
                    normal = Vector2.up;
                    contactPoint = new Vector2(body.transform.position.x, wall.line.GetPosition(0).y);
                    isColliding = true;
                }
                break;
            case eduWallCollider.Wallside.Top:
                if (body.transform.position.y + circle.radius > wall.line.GetPosition(0).y)
                {
                    normal = Vector2.down;
                    contactPoint = new Vector2(body.transform.position.x, wall.line.GetPosition(0).y);
                    isColliding = true;
                }
                break;
        }

        if (isColliding)
        {
            float penetration = circle.radius - Vector2.Distance(contactPoint, body.transform.position);
            body.transform.position = body.transform.position + (Vector3)(normal * penetration);

            Vector2 relativeVelocity = body.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, normal);
            if (normalVelocity < 0)
            {
                Vector2 impulse = -(1 + body.restitution) * normalVelocity * normal * body.mass;
                body.velocity += impulse / body.mass;
            }
        }
    }

    void CircleCircleCollision(eduRigidBody bodyA, eduRigidBody bodyB)
    {
        circleA = bodyA.GetComponent<eduCircleCollider>();
        circleB = bodyB.GetComponent<eduCircleCollider>();
        if (circleA == null || circleB == null) return;

        Vector2 delta = bodyB.transform.position - bodyA.transform.position;
        float distance = delta.magnitude;
        float radiusSum = circleA.radius + circleB.radius;

        if (distance < radiusSum)
        {
            Vector2 normal = delta.normalized;
            float penetration = radiusSum - distance;

            bodyA.transform.position -= (Vector3)(normal * penetration * 0.5f);
            bodyB.transform.position += (Vector3)(normal * penetration * 0.5f);

            Vector2 relativeVelocity = bodyB.velocity - bodyA.velocity;
            float normalVelocity = Vector2.Dot(relativeVelocity, normal);

            if (normalVelocity < 0)
            {
                float e = Mathf.Min(bodyA.restitution, bodyB.restitution);
                float impulseMagnitude = -(1 + e) * normalVelocity / (1 / bodyA.mass + 1 / bodyB.mass);

                Vector2 impulse = impulseMagnitude * normal;

                bodyA.velocity -= impulse / bodyA.mass;
                bodyB.velocity += impulse / bodyB.mass;

                float ERP = 0.5f;
                Vector2 correction = ERP * penetration * normal;
                bodyA.transform.position -= (Vector3)(correction * (bodyA.mass / (bodyA.mass + bodyB.mass)));
                bodyB.transform.position += (Vector3)(correction * (bodyB.mass / (bodyA.mass + bodyB.mass)));
            }
        }
    }
}
