using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static eduWallCollider;

public class eduWallCollider : MonoBehaviour
{
    public LineRenderer line;
    public Vector3[] wallPos;
    public Vector3 leftVec, rightVec, topVec, bottomVec;


    public enum Wallside 
    { 
        Left, Right, Top, Bottom 
    };

    public Wallside side;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        //wallPos = new Vector3[2];

        //wallPos[0] = line.GetPosition(0);
        wallPos = new Vector3[line.positionCount];

        line.GetPositions(wallPos);

        switch (side)
        {
            case Wallside.Left:
                leftVec = new Vector3(wallPos[0].x, wallPos[1].y - wallPos[0].y, 0);
                break;
            case Wallside.Right:
                rightVec = new Vector3(wallPos[0].x, wallPos[1].y - wallPos[0].y, 0);
                break;
            case Wallside.Top:
                topVec = new Vector3(wallPos[1].x - wallPos[0].x, wallPos[0].y, 0);
                break;
            case Wallside.Bottom:
                bottomVec = new Vector3(wallPos[1].x - wallPos[0].x, wallPos[0].y, 0);
                break;
            default:
                break;
        }
    }
}
