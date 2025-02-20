using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(eduRigidBody))]
public class eduCircleCollider : MonoBehaviour
{
    public float radius;
    public float density;
    private eduRigidBody rb;

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
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            radius = spriteRenderer.bounds.size.x / 2f;
        }
        else
        {
            radius = transform.localScale.x / 2f;
        }

        rb = GetComponent<eduRigidBody>();
        rb.mass = density * radius * radius * Mathf.PI; // Samma som densitet * area eftersom att det är en cirkel
        rb.inertia = (radius * radius * rb.mass) / 2;
    }

}
