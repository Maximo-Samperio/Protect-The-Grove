using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Dijkstra : MonoBehaviour
{
    Dictionary<Waypoints, float> distances = new Dictionary<Waypoints, float>();
    Dictionary<Waypoints, Waypoints> previous = new Dictionary<Waypoints, Waypoints>();

    public List<Waypoints> CalculateShortestPath(Waypoints start, Waypoints end)
    {
        distances.Clear();
        previous.Clear();

        foreach (Waypoints waypoint in FindObjectsOfType<Waypoints>())
        {
            distances[waypoint] = Mathf.Infinity;
            previous[waypoint] = null;
        }

        distances[start] = 0f;
        HashSet<Waypoints> visited = new HashSet<Waypoints>();
        HashSet<Waypoints> unvisited = new HashSet<Waypoints>();

        unvisited.Add(start);

        while (unvisited.Count > 0)
        {
            Waypoints current = GetClosestWaypoint(unvisited, distances);
            unvisited.Remove(current);
            visited.Add(current);

            foreach (Waypoints neighbor in current.neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    float tentativeDistance = distances[current] + Vector3.Distance(current.transform.position, neighbor.transform.position);

                    if (tentativeDistance < distances[neighbor])
                    {
                        distances[neighbor] = tentativeDistance;
                        previous[neighbor] = current;
                        unvisited.Add(neighbor);
                    }
                }
            }
        }

        return GetShortestPath(end);
    }

    Waypoints GetClosestWaypoint(HashSet<Waypoints> waypoints, Dictionary<Waypoints, float> distances)
    {
        float minDistance = Mathf.Infinity;
        Waypoints closestWaypoint = null;

        foreach (Waypoints waypoint in waypoints)
        {
            if (distances[waypoint] < minDistance)
            {
                minDistance = distances[waypoint];
                closestWaypoint = waypoint;
            }
        }

        return closestWaypoint;
    }

    List<Waypoints> GetShortestPath(Waypoints end)
    {
        List<Waypoints> path = new List<Waypoints>();
        Waypoints current = end;

        while (current != null)
        {
            path.Insert(0, current);
            current = previous[current];
        }

        return path;
    }

}
