using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class eduRigidBody : MonoBehaviour
{
    public Vector2 velocity;
    public float angularVelocity;
    public Vector2 force;
    public float mass;
    public float torque;
    public float inertia;
    public float bounceConstant;

    private void Start()
    {

    }

    private void FixedUpdate() //uppdateras 0.2s istället för beroende på framerate som vanliga update gör
    {
        float deltaTime = Time.fixedDeltaTime; 
        
        float nextVelY = velocity.y + (force.y * deltaTime / mass);
        velocity.y = nextVelY;
        float nextVelX = velocity.x + (force.x * deltaTime / mass);
        velocity.x = nextVelX;

        float nextPosY = transform.position.y + (nextVelY * deltaTime);
        float nextPosX = transform.position.x + (nextVelX * deltaTime);

        transform.position = new Vector3(nextPosX, nextPosY);
        //transform.Translate(velocity.x * deltaTime, velocity.y * deltaTime, 0);

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
