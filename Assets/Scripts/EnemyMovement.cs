using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] movePoints;
    [SerializeField] private float moveSpeed = 2f;

    private int currentTargetIndex;
    private float lerpProgress = 0;
    private Transform currentStart;
    private Transform currentEnd;

    [SerializeField] private bool circleMovement;
    [SerializeField] private Transform circleMovementPosition;
    [SerializeField] private float radius = 5f;
    [SerializeField] private float circleSpeed = 1f;

    private float angle = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (movePoints.Length >= 2)
        {
            currentStart = movePoints[0];
            currentEnd = movePoints[1];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!circleMovement)
        {
            Movement();
        }
        else
        {
            CircleMovement();
        }
    }
    
    private void Movement()
    {
        if (movePoints.Length >= 2)
        {
            lerpProgress += Time.deltaTime * moveSpeed;

            transform.position = Vector3.Lerp(currentStart.position, currentEnd.position, lerpProgress);

            if (lerpProgress >= 1)
            {
                lerpProgress = 0f;
                currentTargetIndex = (currentTargetIndex + 1) % movePoints.Length;
                currentStart = currentEnd;
                currentEnd = movePoints[(currentTargetIndex + 1) % movePoints.Length];
            }
        }

    }

    private void CircleMovement()
    {
        angle += Time.deltaTime * circleSpeed;
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;
        transform.position = new Vector3(circleMovementPosition.position.x + x, circleMovementPosition.position.y, circleMovementPosition.position.z + z);
    }
}
