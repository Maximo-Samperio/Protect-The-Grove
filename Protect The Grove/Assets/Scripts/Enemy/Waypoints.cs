using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] points;

    void Awake ( ) 
    {
        // Here I create one space in the array for each child of Waypoint, I loop through them and then set every place in the array equal to the child
        // Thus, allowing me to have the enemy move without having it find and order the waypoints
        points = new Transform[transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }
}
