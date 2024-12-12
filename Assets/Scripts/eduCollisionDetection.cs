using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eduCollisionDetection : MonoBehaviour
{
    public eduWallCollider[] eduWallColliders;
    public eduCircleCollider[] eduCircleColliders;

    void Start()
    {
        eduWallColliders = FindObjectsByType<eduWallCollider>(FindObjectsSortMode.None);
        eduCircleColliders = FindObjectsByType<eduCircleCollider>(FindObjectsSortMode.None);
    }

    void FixedUpdate()
    {
        //foreach (var circleCol in eduCircleColliders)
        //{

        //}
    }
}
