using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduForces : MonoBehaviour
{
    //need to add flags and values to turn on/off forces

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
            gravityForce = new Vector2(0f, rb.mass * -9.82f);
            
            //Debug.Log(gravityForce + " gravityforce ");

            rb.applyForce(gravityForce); 
            rb.applyTorque(torque); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //If needed: draw lines etc to visualize forces here.
    }
}
