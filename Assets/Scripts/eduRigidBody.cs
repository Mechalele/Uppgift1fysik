using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class eduRigidBody : MonoBehaviour
{
    //velocities
    public Vector2 velocity;
    public float startAngularVelocity;
    public float updatingAngularVelocity;
    public float totalAngularVelocity;

    //forces
    public Vector2 force;
    public float mass;
    public float torque; //vrid/kraft(?) moment
    public float inertia; //tröghets moment
    public float bounceConstant;
    public float restitution; //Cr 
    
    //add ons
    public int frameSkip;
    public float deltaTime;
    public float timer;

    private void Start()
    {
        deltaTime = Time.fixedDeltaTime * (frameSkip + 1);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        if (timer > deltaTime)
        {
            MoveObj();
            SpinObj();
            timer = Time.fixedDeltaTime;
        }
        
        force.x = 0;
        force.y = 0;
        torque = 0;
    }

    private void SpinObj()
    {
        //Debug.Log(updatingAngularVelocity + " startAngularVelocity " + startAngularVelocity + " totalvel " + totalAngularVelocity + " torque " + torque + " inertia " + inertia);

        updatingAngularVelocity += torque * deltaTime / inertia;
        
        //Debug.Log(" updtvel " + updatingAngularVelocity + " for " + transform.name);
        
        totalAngularVelocity = updatingAngularVelocity + startAngularVelocity;
        
        transform.Rotate(0, 0, totalAngularVelocity);
    }

    private void MoveObj()
    {
        //Debug.Log(" y led " + force.y + " x led " + force.x + " accel " + force.y / mass + " mass " + mass);

        //v(t) = v0 + at, a = F/m = -9.82
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
        //Debug.Log(" applied torque = " + torque + " add on new torque " + t);
    }

    public void applyImpulse(Vector2 j)
    {
        
    }
}
