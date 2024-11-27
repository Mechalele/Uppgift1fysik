using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticalTrajectory : MonoBehaviour
{
    public Vector3 startPos = new Vector3(0, 0);
    public Vector3 currentPos = new Vector3(0, 0);
    public Vector3 startVel = new Vector3(0, 0);
    public Vector3 acceleration = new Vector3(0, -9.82f);


    public float timeVar;

    private void Start()
    {
        currentPos = startPos;

        //Application.targetFrameRate = 144;
    }


    void FixedUpdate()
    {
        

         
    }

}
