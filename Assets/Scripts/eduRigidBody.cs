using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class eduRigidBody : MonoBehaviour
{
    public Vector3 startPos = new Vector3(0, 0);
    public Vector3 currentPos = new Vector3(0, 0);

    public Vector2 velocity;
    public float angularVelocity;
    public Vector2 force;
    public float mass;
    public float torque;
    public float inertia;
    public float bounceConstant;
    public float restitution;
    public int frameSkip;

    private void Start()
    {

        startPos = currentPos;

        Debug.Log(Time.fixedDeltaTime);
        //är detta rätt? ser inte ut och matcha bild på 1.2
        Time.fixedDeltaTime = Time.fixedDeltaTime * (frameSkip + 1);

        Debug.Log(Time.fixedDeltaTime);
    }

    private void FixedUpdate() //uppdateras 0.2s istället för beroende på framerate som vanliga update gör
    {
        AnalyticalTrajectory();

        MoveObj();

        Debug.Log("Fixedupdate " + gameObject.name);

        //lägg till angularvel 

        force.x = 0;
        force.y = 0;
        torque = 0;
    }

    private void AnalyticalTrajectory()
    {
        Debug.Log(force.y);
        Vector3 trajectory = new Vector3(startPos.x + velocity.x * Time.fixedDeltaTime, startPos.y + velocity.y * Time.fixedDeltaTime + -9.82f * Mathf.Pow(Time.fixedDeltaTime, 2) / 2);

        Debug.DrawLine(startPos, trajectory, Color.white, 5f);
    }

    private void MoveObj()
    {
        //v = v + at, a = F/m
        float nextVelY = velocity.y + (force.y / mass) * Time.fixedDeltaTime;
        velocity.y = nextVelY;

        float nextVelX = velocity.x + (force.x / mass) * Time.fixedDeltaTime;
        velocity.x = nextVelX;

        float nextPosY = transform.position.y + (nextVelY * Time.fixedDeltaTime);
        float nextPosX = transform.position.x + (nextVelX * Time.fixedDeltaTime);

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
