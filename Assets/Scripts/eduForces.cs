using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduForces : MonoBehaviour
{
    eduRigidBody[] rbs;
    Vector2 gravityForce;
    float torque;

    // Start is called before the first frame update
    void Start()
    {
        rbs = FindObjectsByType<eduRigidBody>(FindObjectsSortMode.None);
        
    }

    private void FixedUpdate()
    {
        foreach (var rb in rbs)
        {
            gravityForce = new(0f, rb.mass * 9.82f);
            
            rb.applyForce(gravityForce); 
            rb.applyTorque(torque); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
