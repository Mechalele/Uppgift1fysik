using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduForces : MonoBehaviour
{
    public eduRigidBody[] rbs;
    public eduCircleCollider[] colliders;

    //Gravity
    public bool gravityOn = true;
    public Vector2 gravityForce = new Vector2(0f, -9.82f);
    
    //Torque
    public bool torqueOn = true;
    public float torque;

    //Wind
    public bool windOn = true;
    public float windSpeed;
    public float airdensityVariable;

    //Bouyancy
    public Material buoyancyMaterial;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private eduWallCollider bottomWall;
    private eduWallCollider leftWall;
    private eduWallCollider rightWall;

    void Start()
    {
        eduWallCollider[] walls = FindObjectsOfType<eduWallCollider>();
        foreach (eduWallCollider wall in walls)
        {
            if (wall.side == eduWallCollider.Wallside.Bottom)
                bottomWall = wall;
            else if (wall.side == eduWallCollider.Wallside.Left)
                leftWall = wall;
            else if (wall.side == eduWallCollider.Wallside.Right)
                rightWall = wall;
        }

        rbs = FindObjectsByType<eduRigidBody>(FindObjectsSortMode.None);
    }

    private void FixedUpdate()
    {
        foreach (var rb in rbs)
        {
            if (gravityOn == true)
            {
                rb.ApplyForce(gravityForce * rb.mass);
            }

            if (torqueOn == true)
            {
                rb.ApplyTorque(torque);
            }
        }
        
    }

    //void ApplyWindForce(eduRigidBody body)
    //{
    //    float area = body.transform.localScale.x * body.transform.localScale.y;

    //    Vector2 variableWind = windDirection * windStrength + new Vector2(
    //        Random.Range(-windVariation.x, windVariation.x),
    //        Random.Range(-windVariation.y, windVariation.y)
    //    );

    //    Vector2 windForce = variableWind * area;
    //    body.ApplyForce(windForce);
    //}

    //void ApplyBuoyancyForce(eduRigidBody body)
    //{
    //    eduCircleCollider circle = body.GetComponent<eduCircleCollider>();
    //    if (circle == null) return;

    //    float depth = fluidLevel - body.transform.position.y;
    //    if (depth > 0)
    //    {
    //        float submergedArea = Mathf.PI * circle.radius * circle.radius;

    //        float buoyancyForceMagnitude = fluidDensity * submergedArea * depth * gravity.y;
    //        Vector2 buoyancyForce = new Vector2(0, -buoyancyForceMagnitude);

    //        body.ApplyForce(buoyancyForce);
    //    }
    //}

    //void UpdateBuoyancyArea()
    //{

    //    if (meshFilter == null || bottomWall == null || leftWall == null || rightWall == null) return;

    //    float bottomWallY = bottomWall.transform.position.y;
    //    float leftWallX = leftWall.transform.position.x;
    //    float rightWallX = rightWall.transform.position.x;

    //    float height = fluidLevel - bottomWallY;
    //    if (height < 0) height = 0;

    //    Vector3[] vertices = new Vector3[4];
    //    vertices[0] = new Vector3(leftWallX, bottomWallY, 0);
    //    vertices[1] = new Vector3(rightWallX, bottomWallY, 0);
    //    vertices[2] = new Vector3(leftWallX, bottomWallY + height, 0);
    //    vertices[3] = new Vector3(rightWallX, bottomWallY + height, 0);

    //    int[] triangles = new int[6] { 0, 2, 1, 1, 2, 3 };

    //    Mesh mesh = new Mesh();
    //    mesh.vertices = vertices;
    //    mesh.triangles = triangles;
    //    mesh.RecalculateNormals();

    //    meshFilter.mesh = mesh;

    //}
}
