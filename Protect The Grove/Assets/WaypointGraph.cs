using UnityEngine;
using System.Collections.Generic;

public class WaypointGraph : MonoBehaviour
{
    //public static WaypointGraph instance; // Singleton instance

    //public List<Waypoints> waypoints;

    //void Awake()
    //{
    //    // Ensure only one instance of WaypointGraphManager exists
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //// Add a method to find the next waypoint based on the current waypoint
    //public Waypoints GetNextWaypoint(Waypoints currentWaypoint)
    //{
    //    if (currentWaypoint.connectedWaypoints.Length > 0)
    //    {
    //        int randomIndex = Random.Range(0, currentWaypoint.connectedWaypoints.Length);
    //        Waypoints nextWaypoint = currentWaypoint.connectedWaypoints[randomIndex];
    //        Debug.Log("Next waypoint: " + nextWaypoint.gameObject.name);
    //        return nextWaypoint;
    //    }

    //    Debug.LogWarning("No connected waypoints for: " + currentWaypoint.gameObject.name);
    //    return null;
    //}
}
