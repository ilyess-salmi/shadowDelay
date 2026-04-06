using UnityEngine;

public class SpikeButton : MonoBehaviour
{
    public GameObject spikes;

    private int objectsOnButton = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            objectsOnButton++;

            if (spikes != null)
                spikes.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Shadow"))
        {
            objectsOnButton--;

            if (objectsOnButton <= 0 && spikes != null)
                spikes.SetActive(true);
        }
    }
}