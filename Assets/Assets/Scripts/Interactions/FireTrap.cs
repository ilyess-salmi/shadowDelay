using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    public Transform respawnPoint;
    public float respawnDelay = 0.8f;
    public float deathRotationZ = -90f;

    private bool isRespawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning)
            return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(RespawnPlayer(other.gameObject));
        }
    }

    private IEnumerator RespawnPlayer(GameObject player)
    {
        isRespawning = true;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        CharacterMovement movement = player.GetComponent<CharacterMovement>();

        if (movement != null)
            movement.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        player.transform.rotation = Quaternion.Euler(0f, 0f, deathRotationZ);

        yield return new WaitForSeconds(respawnDelay);

        if (respawnPoint != null)
            player.transform.position = respawnPoint.position;

        player.transform.rotation = Quaternion.identity;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        if (movement != null)
            movement.enabled = true;

        isRespawning = false;
    }
}