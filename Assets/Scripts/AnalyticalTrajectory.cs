using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticalTrajectory : MonoBehaviour
{
    public Vector3 startPos = new Vector3(0, 0);
    public Vector3 lastPos;
    public Vector3 currentPos = new Vector3(0, 0);
    public Vector3 startVel = new Vector3(0, 0);
    public Vector3 acceleration = new Vector3(0, -9.82f);


    public float timeVar;

    private void Start()
    {
        lastPos = startPos;

        //Application.targetFrameRate = 144;
    }

    //fixed vs vanlig update? fixeddeltatime är ändrad för alla obj
    void Update()
    {

        timeVar += Time.deltaTime;

        currentPos = new Vector3(startPos.x + startVel.x * timeVar, startPos.y + startVel.y * timeVar + -9.82f * Mathf.Pow(timeVar, 2) / 2);

        //Debug.Log("current pos " + currentPos + " vel " + startVel);

        Debug.DrawLine(lastPos, currentPos, Color.white, 10f);

        lastPos = currentPos;


    }

    //void FixedUpdate()
    //{

    //    Debug.Log(Time.fixedDeltaTime);

    //    timeVar += Time.fixedDeltaTime;

    //    currentPos = new Vector3(startPos.x + startVel.x * timeVar, startPos.y + startVel.y * timeVar + -9.82f * Mathf.Pow(timeVar, 2) / 2);

    //    //Debug.Log("current pos " + currentPos + " vel " + startVel);

    //    Debug.DrawLine(lastPos, currentPos, Color.white, 10f);

    //    lastPos = currentPos;


    //}

}
