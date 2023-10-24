using UnityEngine;
using System.Collections.Generic;

public class WaypointGraph : MonoBehaviour
{
    public List<Waypoints> waypoints = new List<Waypoints>();

    void Start()
    {
        //PopulateWaypoints();
        ConnectWaypoints();
    }

    //void PopulateWaypoints()
    //{
    //    // Automatically create waypoints based on child Transforms
    //    for (int i = 0; i < transform.childCount; i++)
    //    {
    //        Transform child = transform.GetChild(i);
    //        waypoints.Add(child.GetComponent<Waypoints>());
    //    }
    //}

    void ConnectWaypoints()
    {
        // Automatically connect waypoints based on proximity
        float maxConnectionDistance = 20f;

        for (int i = 0; i < waypoints.Count; i++)
        {
            Waypoints waypointA = waypoints[i];

            for (int j = i + 1; j < waypoints.Count; j++)
            {
                Waypoints waypointB = waypoints[j];
                float distance = Vector3.Distance(waypointA.Position, waypointB.Position);

                if (distance <= maxConnectionDistance)
                {
                    waypointA.AddNeighbor(waypointB);
                }
            }
        }
    }
}
