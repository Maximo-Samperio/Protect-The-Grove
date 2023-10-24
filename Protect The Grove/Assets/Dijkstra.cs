using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Dijkstra : MonoBehaviour
{
    public static List<Waypoints> FindShortestPath(Waypoints start, Waypoints end)
    {
        Dictionary<Waypoints, float> distances = new Dictionary<Waypoints, float>();
        Dictionary<Waypoints, Waypoints> previous = new Dictionary<Waypoints, Waypoints>();
        List<Waypoints> unvisited = new List<Waypoints>();

        distances[start] = 0;

        foreach (Waypoints waypoint in start.Neighbors)
        {
            distances[waypoint] = Vector3.Distance(start.Position, waypoint.Position);
            previous[waypoint] = start;
            unvisited.Add(waypoint);
        }

        while (unvisited.Count > 0)
        {
            Waypoints currentWaypoint = unvisited.OrderBy(waypoint => distances[waypoint]).First();
            unvisited.Remove(currentWaypoint);

            if (currentWaypoint == end)
                break;

            foreach (Waypoints neighbor in currentWaypoint.Neighbors)
            {
                float alt = distances[currentWaypoint] + Vector3.Distance(currentWaypoint.Position, neighbor.Position);
                if (alt < distances[neighbor])
                {
                    distances[neighbor] = alt;
                    previous[neighbor] = currentWaypoint;
                }
            }
        }

        List<Waypoints> path = new List<Waypoints>();
        Waypoints current = end;

        while (current != null)
        {
            path.Insert(0, current);
            current = previous.ContainsKey(current) ? previous[current] : null;
        }

        return path;
    }
}
