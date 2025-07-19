using UnityEngine;
using System.Collections.Generic;

public class PathTestRunner : MonoBehaviour
{
    public DijkstraPathFinding pathFinder;
    public PathMover pathMover;

    public Transform startNode;
    public Transform endNode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startNode != null && endNode != null)
        {
            List<Transform> path = pathFinder.FindShortestPath(startNode, endNode);
            pathMover.StartPath(path);
        }
        else
        {
            Debug.LogError("Start or End node is not set.");
        }
    }
}
