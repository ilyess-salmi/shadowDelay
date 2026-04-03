using System.Collections.Generic;
using UnityEngine;

public class ShadowFollower : MonoBehaviour
{
    public Transform target;
    public float delaySeconds = 2f;

    private Queue<Vector3> positionHistory = new Queue<Vector3>();
    private Rigidbody2D rb;
    private int delayFrames;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        delayFrames = Mathf.RoundToInt(delaySeconds / Time.fixedDeltaTime);
    }

    void FixedUpdate()
    {
        if (target == null)
            return;

        positionHistory.Enqueue(target.position);

        if (positionHistory.Count > delayFrames)
        {
            Vector3 delayedPosition = positionHistory.Dequeue();

            if (rb != null)
                rb.MovePosition(delayedPosition);
            else
                transform.position = delayedPosition;
        }
    }
}