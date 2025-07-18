using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class CityEdgeLine : MonoBehaviour
{
    public Transform headNode; // Reference to the starting node of the edge
    public Transform tailNode; // Reference to the ending node of the edge

    public Color lineColor = Color.red; // Color of the line
    public float lineWidth = 0.1f; // Width of the line

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (headNode != null && tailNode != null)
        {
            lineRenderer.SetPosition(0, headNode.position); // Set the start position of the line
            lineRenderer.SetPosition(1, tailNode.position); // Set the end position of the line
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

