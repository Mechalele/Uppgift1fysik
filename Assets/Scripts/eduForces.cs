using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class eduForces : MonoBehaviour
{
    public eduRigidBody[] rbs;
    public eduCircleCollider[] colliders;
    public eduWallCollider[] walls;

    //Gravity
    public bool gravityOn = true;
    public Vector2 gravityForce = new Vector2(0, 0);
    
    //Torque
    public bool torqueOn = true;
    public float torque;

    //Wind
    public bool windOn = true;
    public float windSpeed;
    public float airdensityVariable;
    public Vector2 windDirection = new Vector2(0, 0);
    public Vector2 windVariation = new Vector2(0, 0);

    //Bouyancy
    public bool waterOn = true;
    public Material buoyancyMaterial;
    public float fluidLevel;
    public float fluidDensity;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;
    private eduWallCollider bottomWall;
    private eduWallCollider leftWall;
    private eduWallCollider rightWall;

    private void Awake()
    {
        
    }

    void Start()
    {
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>(); //awake vs start? renderingen behövs fixas
        meshRenderer.material = buoyancyMaterial;

        walls = FindObjectsOfType<eduWallCollider>();
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

            if (windOn == true)
            {
                ApplyWindForce(rb);
            }

            if (waterOn == true)
            {
                ApplyBuoyancyForce(rb);
            }
        }
    }

    void ApplyWindForce(eduRigidBody body)
    {
        float area = body.transform.localScale.x * body.transform.localScale.y; //ska ändras till väggområdet(?)
        //Debug.Log(area);

        Vector2 variableWind = windDirection * windSpeed + new Vector2(
            Random.Range(-windVariation.x, windVariation.x),
            Random.Range(-windVariation.y, windVariation.y)
        );

        Vector2 windForce = variableWind * area;
        //Debug.Log(windForce);
        body.ApplyForce(windForce);
    }

    private void Update()
    {
        //keys to steer wind direction and speed
        if (Input.GetKeyDown("up"))
        {
            windDirection = new Vector2(0, 1);
            Debug.Log("wind = upp");
        }
        else if (Input.GetKeyDown("down"))
        {
            windDirection = new Vector2(0, -1);

        }
        else if (Input.GetKeyDown("left"))
        {
            windDirection = new Vector2(-1, 0);

        }
        else if (Input.GetKeyDown("right"))
        {
            windDirection = new Vector2(1, 0);
        }
        else if (Input.GetKeyDown("d"))
        {
            windDirection = new Vector2(1, 1);
        }
        else if (Input.GetKeyDown("f"))
        {
            windDirection = new Vector2(-1, -1);
        }
        else if(Input.GetKeyDown("s"))
        {
            windSpeed += 10f;
        }

        if (waterOn == true) //ska kunna uppdateras i runtime annars kan den va i start
        {
            UpdateBuoyancyArea();
        }
    }

    void ApplyBuoyancyForce(eduRigidBody body)
    {
        eduCircleCollider circle = body.GetComponent<eduCircleCollider>();
        if (circle == null) return;

        float depth = fluidLevel - body.transform.position.y;
        if (depth > 0)
        {
            float submergedArea = Mathf.PI * circle.radius * circle.radius;

            float buoyancyForceMagnitude = fluidDensity * submergedArea * depth * gravityForce.y;
            Vector2 buoyancyForce = new Vector2(0, -buoyancyForceMagnitude);

            body.ApplyForce(buoyancyForce);
        }
    }

    void UpdateBuoyancyArea()
    {
        if (meshFilter == null || bottomWall == null || leftWall == null || rightWall == null)
            return;

        Mesh mesh = new Mesh();
        
        //Tror att det är detta som gör att renderern inte funkar men idk
        float bottomWallY = bottomWall.transform.position.y;
        float leftWallX = leftWall.transform.position.x;
        float rightWallX = rightWall.transform.position.x;

        //Debug.Log(bottomWallY); //den tar walls position inte bottomwall

        Debug.Log(leftWallX);

        float height = fluidLevel - bottomWallY;

        if (height < 0)
            height = 0;

        Vector3[] vertices = new Vector3[4];
        vertices[0] = new Vector3(leftWallX, bottomWallY, 0);
        vertices[1] = new Vector3(rightWallX, bottomWallY, 0);
        vertices[2] = new Vector3(leftWallX, bottomWallY + height, 0);
        vertices[3] = new Vector3(rightWallX, bottomWallY + height, 0);

        int[] triangles = new int[6] { 0, 2, 1, 1, 2, 3 };

        Vector2[] uv = new Vector2[4]
        {
        new Vector2(0,0),
        new Vector2(1,0),
        new Vector2(0,1),
        new Vector2(1,1)
        };

        
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        //något här stämmer inte renderern finns och ser rätt ut dock
        meshFilter.GetComponent<MeshFilter>().mesh = mesh;
        mesh.RecalculateNormals();

        //GetComponent<MeshFilter>().mesh = mesh; //ska den va i update eller start???
    }
}
