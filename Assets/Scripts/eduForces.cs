using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduForces : MonoBehaviour
{
    eduRigidBody[] rbs;

    public bool gravityOn = true; // behövs fixas
    public Vector2 gravityForce;
    
    public bool torqueOn = true;
    public float torque;


    void Start()
    {
        rbs = FindObjectsByType<eduRigidBody>(FindObjectsSortMode.None);
    }

    private void FixedUpdate()
    {
        foreach (var rb in rbs)
        {
            gravityForce = new Vector2(0f, rb.mass * -9.82f);
            
            //Debug.Log(gravityForce + " gravityforce ");

            rb.applyForce(gravityForce); 
            rb.applyTorque(torque); 
        }
        
    }
}
