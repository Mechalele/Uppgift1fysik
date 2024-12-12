using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduWallCollider : MonoBehaviour
{
    private LineRenderer line;
    public Vector3[] wallPos;
    

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        wallPos = new Vector3[5];

        //wallPos[0] = line.GetPosition(0);
        wallPos = new Vector3[line.positionCount];

        line.GetPositions(wallPos);

    }

    void Update()
    {
        
    }
}
