using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Transform playerRespawnPoint;
    public Transform shadowRespawnPoint;

    public float respawnDelay = 0.8f;
    public float deathRotationZ = -90f;

    private bool isRespawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning)
            return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleDeath(other.gameObject, true));
        }
        else if (other.CompareTag("Shadow"))
        {
            // Le shadow ne meurt pas
            Debug.Log("Shadow touched spikes but survives.");
        }
    }

    private IEnumerator HandleDeath(GameObject character, bool isPlayer)
    {
        isRespawning = true;

        Rigidbody2D rb = character.GetComponent<Rigidbody2D>();
        CharacterMovement movement = character.GetComponent<CharacterMovement>();

        if (movement != null)
            movement.enabled = false;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }

        character.transform.rotation = Quaternion.Euler(0f, 0f, deathRotationZ);

        yield return new WaitForSeconds(respawnDelay);

        if (isPlayer && playerRespawnPoint != null)
            character.transform.position = playerRespawnPoint.position;
        else if (!isPlayer && shadowRespawnPoint != null)
            character.transform.position = shadowRespawnPoint.position;

        character.transform.rotation = Quaternion.identity;

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