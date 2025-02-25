using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static eduWallCollider;

public class eduWallCollider : MonoBehaviour
{
    public LineRenderer line;
    public Vector3[] wallPos;
    
    public enum Wallside 
    { 
        Left, Right, Top, Bottom 
    };

    public Wallside side;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        wallPos = new Vector3[2];

        //wallPos[0] = line.GetPosition(0);
        wallPos = new Vector3[line.positionCount];

        line.GetPositions(wallPos);
    }

    void Update()
    {
        Vector3 chosenVec = Vector3.zero;

        switch (side)
        {
            case Wallside.Left:
                chosenVec = new Vector3(wallPos[0].x, wallPos[1].y - wallPos[0].y, 0);
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    Debug.Log(chosenVec + " left");
                }
                break;
            case Wallside.Right:
                chosenVec = new Vector3(wallPos[0].x, wallPos[1].y - wallPos[0].y, 0);
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    Debug.Log(chosenVec + " right");
                }
                break;
            case Wallside.Top:
                chosenVec = new Vector3(wallPos[1].x - wallPos[0].x, wallPos[0].y, 0); 
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    Debug.Log(chosenVec + " top");
                }
                break;
            case Wallside.Bottom:
                chosenVec = new Vector3(wallPos[1].x - wallPos[0].x, wallPos[0].y, 0);
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    Debug.Log(chosenVec + " bottom");
                }
                break;
            default:
                break;
        }

        //Debug.DrawLine(new Vector3(wallPos[0].x, wallPos[0].y), new Vector3(wallPos[1].x, wallPos[1].y), Color.red);

    }
}
