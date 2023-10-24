using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public Vector3 Position { get; private set; }
    public List<Waypoints> Neighbors { get; private set;}


    public void setup (Vector3 position)
    {
        Position = position;
        Neighbors = new List<Waypoints>();
    }

    public void AddNeighbor(Waypoints neighbor)
    {
        if (Neighbors == null)
        {
            Neighbors = new List<Waypoints> ();
        }

        if (!Neighbors.Contains(neighbor))
        {
            Neighbors.Add(neighbor);
            neighbor.AddNeighbor(this);
        }
    }
}
