using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public List<Waypoints> neighbors = new List<Waypoints>();

    // Add neighbor waypoints
    public void AddNeighbor(Waypoints neighbor)
    {
        if (!neighbors.Contains(neighbor))
        {
            neighbors.Add(neighbor);
            neighbor.neighbors.Add(this); // Assuming bidirectional movement
        }
    }
}
