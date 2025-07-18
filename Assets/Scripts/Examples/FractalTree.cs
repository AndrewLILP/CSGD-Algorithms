using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FractalTree : MonoBehaviour
{
    public float minimumScale = 0.1f; // Minimum scale for the branches
    public float fraction = 0.75f; // Fraction of the parent branch length for the child branches
    public GameObject treeNode;
    //public Vector3 startPosition;
    public Vector3 endPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateTree(transform.position, Vector3.zero, 1);
    }

    void GenerateTree(Vector3 startPos, Vector3 angle, float scale)
    {
        if (scale > minimumScale)
        {
            Transform branchNode = Instantiate(treeNode).transform; // Instantiate a new tree node
            branchNode.position = startPos; // Set the position of the branch node
            branchNode.Rotate(angle); // Set the rotation of the branch node
            branchNode.localScale = Vector3.one * scale; // Set the scale of the branch node

            Vector3 branchEndPosition = startPos + branchNode.up * scale; // Calculate the end position of the branch

            // branch1
            GenerateTree(branchEndPosition, branchNode.eulerAngles + new Vector3(30, 0, 0), scale * fraction); // Recursive call for the first child branch

            // branch1
            GenerateTree(branchEndPosition, branchNode.eulerAngles + new Vector3(-30, 0, 0), scale * fraction); // Recursive call for the first child branch
        }
    }
}
