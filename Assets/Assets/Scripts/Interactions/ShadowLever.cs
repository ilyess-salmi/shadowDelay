using UnityEngine;

public class ShadowLever : MonoBehaviour
{
    public DoorAudio doorAudio;
    public AudioClip buttonClickSound;
    public Animator leverAnimator;

    private AudioSource audioSource;
    private bool isActivated = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shadow") && !isActivated)
        {
            // ✅ Vérifie d'abord les diamants
            if (!LevelObjectiveManager.Instance.HasAllDiamonds())
            {
                // 🔊 Son d'erreur ou rien
                Debug.Log("❌ Pas assez de diamants !");
                return;
            }

            isActivated = true;

            // 🎬 Animation lever
            if (leverAnimator != null)
                leverAnimator.SetBool("isActivated", true);

            // 🔊 Son bouton seulement si diamants OK
            if (audioSource != null && buttonClickSound != null)
                audioSource.PlayOneShot(buttonClickSound);

            LevelObjectiveManager.Instance.ActivateLever();

            // 🔊 Son porte
            if (doorAudio != null)
                doorAudio.PlayDoorSequence();
        }
    }

    public void ResetLever()
    {
        isActivated = false;

        if (leverAnimator != null)
            leverAnimator.SetBool("isActivated", false);
    }
}