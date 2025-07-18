using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ClaudeFractalTree : MonoBehaviour
{
    [Header("Tree Parameters")]
    public float minimumScale = 0.1f; // Minimum scale for the branches
    public float fraction = 0.75f; // Fraction of the parent branch length for the child branches
    public float branchLength = 2f; // Base length of branches
    public float angleRange = 30f; // Angle between branches

    [Header("Prefab")]
    public GameObject treeNode; // Should be a cylinder or branch-like object pointing up (Y-axis)

    void Start()
    {
        // Start the tree growing upward
        GenerateTree(transform.position, Vector3.up, 1f);
    }

    void GenerateTree(Vector3 startPos, Vector3 direction, float scale)
    {
        if (scale > minimumScale)
        {
            // Create branch
            GameObject branch = Instantiate(treeNode);
            Transform branchTransform = branch.transform;

            // Position the branch
            branchTransform.position = startPos;

            // Orient the branch in the growth direction
            branchTransform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Scale the branch
            branchTransform.localScale = new Vector3(scale, scale, scale);

            // Calculate the end position of this branch
            Vector3 branchEndPos = startPos + direction * branchLength * scale;

            // Create child branches with different angles
            float nextScale = scale * fraction;

            // Left branch
            Vector3 leftDirection = RotateVectorAroundAxis(direction, Vector3.forward, angleRange);
            GenerateTree(branchEndPos, leftDirection, nextScale);

            // Right branch  
            Vector3 rightDirection = RotateVectorAroundAxis(direction, Vector3.forward, -angleRange);
            GenerateTree(branchEndPos, rightDirection, nextScale);

            // Optional: Add a third branch for more complexity
            Vector3 middleDirection = RotateVectorAroundAxis(direction, Vector3.right, angleRange * 0.5f);
            GenerateTree(branchEndPos, middleDirection, nextScale);
        }
    }

    // Helper function to rotate a vector around an arbitrary axis
    Vector3 RotateVectorAroundAxis(Vector3 vector, Vector3 axis, float angle)
    {
        return Quaternion.AngleAxis(angle, axis) * vector;
    }
}

// Alternative simpler version if you prefer 2D-style branching:
/*
public class FractalTreeSimple : MonoBehaviour
{
    [Header("Tree Parameters")]
    public float minimumScale = 0.1f;
    public float fraction = 0.75f;
    public float branchLength = 2f;
    public float branchAngle = 30f;
    
    [Header("Prefab")]
    public GameObject treeNode;
    
    void Start()
    {
        GenerateTree(transform.position, 90f, 1f); // Start pointing up (90 degrees)
    }
    
    void GenerateTree(Vector3 startPos, float angle, float scale)
    {
        if (scale > minimumScale)
        {
            // Create and position branch
            GameObject branch = Instantiate(treeNode);
            branch.transform.position = startPos;
            branch.transform.rotation = Quaternion.Euler(0, 0, angle);
            branch.transform.localScale = Vector3.one * scale;
            
            // Calculate end position
            float radians = angle * Mathf.Deg2Rad;
            Vector3 endPos = startPos + new Vector3(
                Mathf.Cos(radians) * branchLength * scale,
                Mathf.Sin(radians) * branchLength * scale,
                0
            );
            
            // Create child branches
            float nextScale = scale * fraction;
            GenerateTree(endPos, angle + branchAngle, nextScale); // Left branch
            GenerateTree(endPos, angle - branchAngle, nextScale); // Right branch
        }
    }
}
*/