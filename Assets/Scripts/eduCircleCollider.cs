using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(eduRigidBody))]
public class eduCircleCollider : MonoBehaviour
{
    [SerializeField]
    private float radius;
    [SerializeField]
    private float density;
    private eduRigidBody eduRigidBody;

    // Start is called before the first frame update
    void Start()
    {
        calculateValues();
    }

    private void OnValidate()
    {
        calculateValues();
    }

    private void calculateValues()
    {
        radius = gameObject.transform.localScale.x;
        eduRigidBody = GetComponent<eduRigidBody>();
        eduRigidBody.mass = density * radius * radius * Mathf.PI; // Samma som densitet * area eftersom att det är en cirkel
        eduRigidBody.inertia = (radius * radius * eduRigidBody.mass) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
