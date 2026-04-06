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

    // 🔥 IMPORTANT: cette fonction doit être DANS la classe
    public void ResetShadowTrail(Vector3 newPosition)
    {
        positionHistory.Clear();

        transform.position = newPosition;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        for (int i = 0; i < delayFrames; i++)
        {
            positionHistory.Enqueue(newPosition);
        }
    }
}