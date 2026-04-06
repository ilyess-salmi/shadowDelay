using UnityEngine;

public class SpikeDamage : MonoBehaviour
{
    public Transform playerRespawnPoint;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        other.transform.position = playerRespawnPoint.position;

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = Vector2.zero;
    }
}