using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class eduRigidBody : MonoBehaviour
{
    public Vector3 startPos = new Vector3(0, 0);
    public Vector3 lastPos;

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
    public float timer;

    private void Start()
    {
        lastPos = startPos;

        //Kan inte ha flera bollar igång samtidigt pga att detta ändrar tiden globalt
        //Time.fixedDeltaTime = Time.fixedDeltaTime * (frameSkip + 1);

        //Debug.Log(Time.fixedDeltaTime);

        deltaTime = Time.fixedDeltaTime * (frameSkip + 1);
        //Debug.Log(deltaTime);

    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > deltaTime)
        {

            MoveObj();

            timer = 0;
        }

        //lägg till angularvel 

        force.x = 0;
        force.y = 0;
        torque = 0;
    }

    private void MoveObj()
    {
        //Debug.Log(" y led " + force.y + " x led " + force.x + " accel " + force.y / mass);

        //v = v + at, a = F/m = -9.82
        float nextVelY = velocity.y + (force.y / mass) * deltaTime;
        velocity.y = nextVelY;

        float nextVelX = velocity.x + (force.x / mass) * deltaTime;
        velocity.x = nextVelX;

        float nextPosY = transform.position.y + (nextVelY * deltaTime);
        float nextPosX = transform.position.x + (nextVelX * deltaTime);

        transform.position = new Vector3(nextPosX, nextPosY);

        //velocity.y += (force.y / mass) * deltaTime;
        //velocity.x += (force.x / mass) * deltaTime;

        //Vector3 newPos = transform.position;
        //newPos.y += velocity.y * deltaTime;
        //newPos.x += velocity.x * deltaTime;

        //transform.position = newPos;
    }

    public void applyForce(Vector2 f)
    {
        force += f;
    }

    public void applyTorque(float t)
    {
        torque += t; //gör inget i nuläget
    }

    public void applyImpulse(Vector2 j)
    {
        velocity += j; //gör inget i nuläget
    }
}
