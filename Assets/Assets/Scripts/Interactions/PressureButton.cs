using UnityEngine;

public class PressureButton : MonoBehaviour
{
    [Header("Spikes (optionnel)")]
    public GameObject spikes;
    public bool holdToDisableSpikes = true; // si true → spikes OFF tant que shadow est dessus

    [Header("Box (optionnel)")]
    public Rigidbody2D boxToDrop;
    public bool dropBoxOnce = true;

    private int objectsOnButton = 0;
    private bool boxActivated = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Shadow"))
            return;

        objectsOnButton++;

        // 🔻 Gestion des spikes
        if (spikes != null)
        {
            spikes.SetActive(false);
        }

        // 📦 Gestion de la box
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

        // 🔺 Réactiver spikes si bouton relâché
        if (spikes != null && holdToDisableSpikes && objectsOnButton <= 0)
        {
            spikes.SetActive(true);
        }
    }
}