using UnityEngine;
using TMPro;
[RequireComponent(typeof(LineRenderer))]

public class CityEdgeLine : MonoBehaviour
{
    public Transform headNode; // Reference to the starting node of the edge
    public Transform tailNode; // Reference to the ending node of the edge

    public Color lineColor = Color.red; // Color of the line
    public float lineWidth = 0.1f; // Width of the line

    public float weight; // Weight of the edge, can be used for pathfinding or other purposes
    [SerializeField] private TMP_Text weightTxt; // Text component to display the weight of the edge

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2; // Set the number of positions in the line renderer

        lineRenderer.startWidth = lineWidth; // Set the start width of the line
        lineRenderer.endWidth = lineWidth; // Set the end width of the line
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // Set a default material for the line renderer
        lineRenderer.startColor = lineColor; // Set the start color of the line
        lineRenderer.endColor = lineColor; // Set the end color of the line
    }

    public Transform GetNeighbor(Transform current)
    {
        if (current == headNode) return tailNode;
        if (current == tailNode) return headNode;
        return null; // If the current node is neither head nor tail, return null
    }

    private void FixedUpdate() // looks every 4th frame - should be if moved, then update weight
    {
        if (headNode != null && tailNode != null)
        {
            lineRenderer.SetPosition(0, headNode.position); // Set the start position of the line
            lineRenderer.SetPosition(1, tailNode.position); // Set the end position of the line
        
            weight = Mathf.Round(Vector3.Distance(headNode.position, tailNode.position) * 100f) / 100f;
            weightTxt.text = weight.ToString(); // Update the weight text with the distance between the nodes
            weightTxt.gameObject.transform.position = (headNode.position + tailNode.position) / 2; // Position the weight text in the middle of the line
            weightTxt.transform.rotation = Camera.main.transform.rotation; // Rotate the text to face the camera
        }
    }

    private void OnDrawGizmos()
    {
        if (headNode != null && tailNode != null)
        {
            Gizmos.color = lineColor;
            Gizmos.DrawLine(headNode.position, tailNode.position);
        }
    }
}

