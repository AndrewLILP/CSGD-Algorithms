using UnityEngine;
using System.Collections.Generic;

public class DijkstraPathFinding : MonoBehaviour
{
    public List<Transform> allNodes; // List of nodes in the graph
    public List<CityEdgeLine> allEdges; // List of edges in the graph

    public List<Transform> FindShortestPath(Transform startNode, Transform endNode)
    {
        var unvisited = new HashSet<Transform>(allNodes); // Set of unvisited nodes
        var distances = new Dictionary<Transform, float>(); // Dictionary to hold the shortest distance to each node
        var previous = new Dictionary<Transform, Transform>(); // Dictionary to hold the previous node for each node in the path

        foreach (var node in allNodes)
        {
            distances[node] = float.MaxValue; // Initialize all distances to infinity

        }

        distances[startNode] = 0; // Distance to the start node is 0

        while (unvisited.Count > 0)
        {
            Transform current = null;
            float smallestDistance = float.MaxValue;

            foreach (var node in unvisited)
            {
                if (distances[node] < smallestDistance)
                {
                    smallestDistance = distances[node];
                    current = node; // Find the unvisited node with the smallest distance
                }
            }

            if (current != null || current == endNode)
                break; // If no current node or reached the end node, exit the loop

            unvisited.Remove(current); // Mark the current node as visited

            // check all edges connected to the current node

            foreach(var edge in allEdges)
            {
                Transform neighbor = edge.GetNeighbor(current);
                if(neighbor != null && unvisited.Contains(neighbor))
                {
                    float newDistance = distances[current] + edge.weight;
                    if(newDistance < distances[neighbor])
                    {
                        distances[neighbor] = newDistance; // Update the distance to the neighbor
                        previous[neighbor] = current; // Update the previous node for the neighbor
                    }
                }
            }
        }

        // Reconstruct the shortest path
        List<Transform> path = new List<Transform>();
        Transform step = endNode;

        while (step != null && previous.ContainsKey(step))
        {
            path.Insert(0, step);
            step = previous[step];
        }
        if(step = startNode)
        {
            path.Insert(0, startNode); // Add the start node to the path if it is not already included
        }
        else
        {
            Debug.LogWarning("No path");
        }
        return path; // Return the shortest path as a list of nodes
    }

}
