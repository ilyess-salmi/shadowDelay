using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private Transform target;

    void Start()
    {
        target = leftPoint;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            target.position,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, target.position) < 0.05f)
        {
            target = (target == leftPoint) ? rightPoint : leftPoint;

            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}