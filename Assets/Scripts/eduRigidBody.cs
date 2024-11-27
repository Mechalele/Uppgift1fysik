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
    public float restitution;
    public int frameSkip;
    public float deltaTime;

    private void Start()
    {
        Debug.Log(Time.fixedDeltaTime);
        //är detta rätt? ser inte ut och matcha bild på 1.2
        Time.fixedDeltaTime = Time.fixedDeltaTime * (frameSkip + 1);
        deltaTime = Time.fixedDeltaTime;

        Debug.Log(Time.fixedDeltaTime + " " + deltaTime);
    }

    private void FixedUpdate() //uppdateras 0.2s istället för beroende på framerate som vanliga update gör
    {
        


        MoveObj(deltaTime);

        Debug.Log("Fixedupdate " + gameObject.name);

        //lägg till angularvel 

        force.x = 0;
        force.y = 0;
        torque = 0;
    }

    private void MoveObj(float deltaTime)
    {
        //v = v + at, a = F/m
        float nextVelY = velocity.y + (force.y / mass) * deltaTime;
        velocity.y = nextVelY;

        float nextVelX = velocity.x + (force.x / mass) * deltaTime;
        velocity.x = nextVelX;

        float nextPosY = transform.position.y + (nextVelY * deltaTime);
        float nextPosX = transform.position.x + (nextVelX * deltaTime);

        transform.position = new Vector3(nextPosX, nextPosY);
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
        velocity += j; //gör inget i nuläget
    }
}
