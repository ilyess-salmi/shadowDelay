using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeTrap : MonoBehaviour
{
    [Header("Respawn Points")]
    public Transform playerRespawnPoint;
    public Transform shadowRespawnPoint;

    [Header("Death Settings")]
    public float respawnDelay = 0.8f;
    public float deathRotationZ = -90f;

    [Header("Lives System")]
    public GameObject heart1;
    public GameObject heart2;
    private int lives = 2;

    [Header("Sounds")]
    public AudioClip hurtSound;
    public AudioClip gameOverSound;
    private AudioSource audioSource;

    [Header("UI")]
    public GameObject gameOverPanel;

    private bool isRespawning = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // ✅ Vérifie si le panel est bien assigné
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
            Debug.Log("GameOverPanel trouvé ✅");
        }
        else
        {
            Debug.LogError("GameOverPanel est NULL ❌ — glisse le panel dans l'Inspector !");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isRespawning)
            return;

        if (other.CompareTag("Player"))
        {
            lives--;
            Debug.Log("Vies restantes : " + lives); // 👈 check dans Console

            if (lives == 1)
            {
                if (hurtSound != null)
                    audioSource.PlayOneShot(hurtSound);

                if (heart2 != null)
                    heart2.SetActive(false);

                StartCoroutine(HandleDeath(other.gameObject));
            }
            else if (lives <= 0)
            {
                if (heart1 != null)
                    heart1.SetActive(false);

                StartCoroutine(GameOver());
            }
        }
    }

    private IEnumerator GameOver()
    {
        isRespawning = true;
        Debug.Log("GAME OVER !"); // 👈 check dans Console

        if (gameOverSound != null)
            audioSource.PlayOneShot(gameOverSound);

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Debug.Log("Panel activé ✅");
        }
        else
        {
            Debug.LogError("Panel est NULL au moment du Game Over ❌");
        }

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private IEnumerator HandleDeath(GameObject player)
    {
        isRespawning = true;

        Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
        PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();

        if (playerMovement != null)
            playerMovement.enabled = false;

        if (playerRb != null)
        {
            playerRb.linearVelocity = Vector2.zero;
            playerRb.angularVelocity = 0f;
        }

        player.transform.rotation = Quaternion.Euler(0f, 0f, deathRotationZ);

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

        yield return new WaitForSeconds(respawnDelay);

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

        if (shadow != null && shadowRespawnPoint != null)
        {
            ShadowFollower shadowFollower = shadow.GetComponent<ShadowFollower>();

            if (shadowFollower != null)
            {
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