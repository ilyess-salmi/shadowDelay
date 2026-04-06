using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PressureButton : MonoBehaviour
{
    [Header("Spikes (optionnel)")]
    public GameObject spikes;
    public bool holdToDisableSpikes = true;

    [Header("Box (optionnel)")]
    public Rigidbody2D boxToDrop;
    public bool dropBoxOnce = true;

    [Header("Audio")]
    public AudioClip buttonPressSound;   // son quand on appuie
    public AudioClip buttonReleaseSound; // son quand on relâche

    private AudioSource audioSource;
    private int objectsOnButton = 0;
    private bool boxActivated = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shadow"))
            return;

        objectsOnButton++;

        // Son d'appui
        if (buttonPressSound != null && audioSource != null)
            audioSource.PlayOneShot(buttonPressSound);

        // Gestion des spikes
        if (spikes != null)
            spikes.SetActive(false);

        // Gestion de la box
        if (boxToDrop != null && (!dropBoxOnce || !boxActivated))
        {
            boxActivated = true;
            boxToDrop.simulated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Shadow"))
            return;

        objectsOnButton--;

        // Réactiver spikes si bouton relâché
        if (spikes != null && holdToDisableSpikes && objectsOnButton <= 0)
        {
            spikes.SetActive(true);

            // Son de relâchement
            if (buttonReleaseSound != null && audioSource != null)
                audioSource.PlayOneShot(buttonReleaseSound);
        }
    }
}