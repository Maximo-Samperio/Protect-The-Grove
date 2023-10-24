using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Vector3 Position { get; }
    public List<Waypoints> Neighbors { get; }

    public Waypoints(Vector3 position)
    {
        Position = position;
        Neighbors = new List<Waypoints>();
    }

    public void AddNeighbor(Waypoints neighbor)
    {
        if (!Neighbors.Contains(neighbor))
        {
            Neighbors.Add(neighbor);
            neighbor.AddNeighbor(this);
        }
    }
}
