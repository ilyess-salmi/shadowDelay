using System.Collections;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [Header("Respawn Points")]
    public Transform playerRespawnPoint;
    public Transform shadowRespawnPoint;

    [Header("Death Settings")]
    public float respawnDelay = 0.8f;
    public float deathRotationZ = -90f;

    private bool isRespawning = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning)
            return;

        if (other.CompareTag("Player"))
        {
            StartCoroutine(HandleDeath(other.gameObject));
        }
    }

    private IEnumerator HandleDeath(GameObject player)
    {
        isRespawning = true;

        // ===== PLAYER =====
        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }

        // rotation 90°
        player.transform.rotation = Quaternion.Euler(0f, 0f, deathRotationZ);

        // ===== SHADOW =====
        GameObject shadow = GameObject.FindGameObjectWithTag("Shadow");

        if (shadow != null)
        {
            ShadowFollower shadowFollower = shadow.GetComponent<ShadowFollower>();
            Rigidbody2D shadowRb = shadow.GetComponent<Rigidbody2D>();

            if (shadowFollower != null)
                shadowFollower.enabled = false;

            if (shadowRb != null)
            {
                shadowRb.linearVelocity = Vector2.zero;
                shadowRb.angularVelocity = 0f;
            }
        }

        // attendre un peu
        yield return new WaitForSeconds(respawnDelay);

        // ===== RESET PLAYER =====
        if (playerRespawnPoint != null)
            player.transform.position = playerRespawnPoint.position;

        player.transform.rotation = Quaternion.identity;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }

        if (playerMovement != null)
            playerMovement.enabled = true;

        // ===== RESET SHADOW (IMPORTANT) =====
        if (shadow != null && shadowRespawnPoint != null)
        {
            ShadowFollower shadowFollower = shadow.GetComponent<ShadowFollower>();

            if (shadowFollower != null)
            {
                // 🔥 reset du trail (fix du bug que tu avais)
                shadowFollower.ResetShadowTrail(shadowRespawnPoint.position);
                shadowFollower.enabled = true;
            }
            else
            {
                shadow.transform.position = shadowRespawnPoint.position;
            }

            shadow.transform.rotation = Quaternion.identity;
        }

        isRespawning = false;
    }
}