using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduWallCollider : MonoBehaviour
{
    private LineRenderer line;
    public Vector3[] wallPos;
    
    public enum wallSide 
    { 
        Left, Right, Top, Bottom 
    };

    public wallSide side;

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
        Vector3 topVec = new Vector3(wallPos[1].x - wallPos[0].x, wallPos[1].y - wallPos[0].y, 0);
        Vector3 rightVec = new Vector3(wallPos[2].x - wallPos[1].x, wallPos[2].y - wallPos[1].y, 0);
        Vector3 bottomVec = new Vector3(wallPos[3].x - wallPos[2].x, wallPos[3].y - wallPos[2].y, 0);
        Vector3 leftVec = new Vector3(wallPos[4].x - wallPos[3].x, wallPos[4].y - wallPos[3].y, 0);
        Vector3 chosenVec;

        switch (side)
        {
            case wallSide.Left:
                chosenVec = leftVec;
                break;
            case wallSide.Right:
                chosenVec = rightVec;
                break;
            case wallSide.Top:
                chosenVec = topVec;
                break;
            case wallSide.Bottom:
                chosenVec = bottomVec;
                break;
            default:
                break;
        }



        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log(topVec + " top");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log(bottomVec + " bottom");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log(leftVec + " left");
        }
        else if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            Debug.Log(rightVec + " right");
        }
    }
}
