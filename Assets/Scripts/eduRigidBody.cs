using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduRigidBody : MonoBehaviour
{
    private Transform transform;
    public Vector2 velocity;
    public float angularVelocity;
    public Vector2 force;
    public float mass;
    public float torque;
    public float inertia;
    public float bounceConstant;
    public float deltaTime;

    private void FixedUpdate()
    {
        deltaTime = Time.fixedDeltaTime; //uppdateras 0.2s istället för beroende på framerate som vanliga update gör
        double nextVelY = velocity.y + (force.y / mass) * deltaTime;
        double nextVelX = velocity.x + (force.x / mass) * deltaTime;
        double nextPosY = transform.position.y + nextVelY * deltaTime;
        double nextPosX = transform.position.x + nextVelX * deltaTime;

        force.x = 0;
        force.y = 0;
        torque = 0;
    }

    public void applyForce(Vector2 f)
    {
        force += f;
    }
    public void applyTorque(float t)
    {
        torque += t;
    }

    public void applyImpulse(Vector2 j)
    {
        velocity += j; //??
    }
}
