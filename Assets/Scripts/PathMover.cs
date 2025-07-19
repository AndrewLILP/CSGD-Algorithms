using UnityEngine;
using System.Collections.Generic;

public class PathMover : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed at which the object moves along the path
    private List<Transform> path; // List of points defining the path
    private int currentIndex = 0; // Index of the current point in the path
    private bool isMoving = false; // Flag to check if the object is currently moving

    public void StartPath(List<Transform> newPath)
    {
        if(newPath == null || newPath.Count == 0)
            return;
        path = newPath;
        currentIndex = 0; // Reset to the first point
        isMoving = true; // Start moving along the path

    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving || path == null || currentIndex >= path.Count)
            return;

        Transform target = path[currentIndex];
        transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target.position) < 0.01f)
        {
            currentIndex++;
            if (currentIndex >= path.Count)
            {
                isMoving = false;
                Debug.Log("Reached the end of the path.");
            }
        }
    
    }
}
