using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FractalTree : MonoBehaviour
{
    public float minimumScale=0.1f;
    public float fraction = 0.75f;
    public GameObject treeNode;
    public Vector3 startPosition;
    public Vector3 startDirection;

    void Start()
    {
        GenerateTree(startPosition, Vector3.zero, 1);
    }

    void GenerateTree(Vector3 startPosition, Vector3 angle, float scale)
    {
        if(scale > minimumScale)
        {
            Transform treenode = Instantiate(treeNode).transform;
            treenode.position = startPosition;
            treenode.Rotate(angle);
            treenode.localScale = Vector3.one * scale;

            Vector3 nodeEndPosition = startPosition + treenode.up * scale;

            //Branch 1
            GenerateTree(nodeEndPosition, treenode.eulerAngles + new Vector3(30, 0, 0), scale * fraction);
            // Branch 2
            GenerateTree(nodeEndPosition, treenode.eulerAngles + new Vector3(-30, 0, 0), scale * fraction);
        }
    }
}
