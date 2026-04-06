using UnityEngine;

public class DoorAudio : MonoBehaviour
{
    public AudioClip doorOpenSound;
    public AudioClip doorCloseSound;
    public float delayBeforeClose = 2f;

    public ShadowLever shadowLever; // 🎬 AJOUTÉ

    private AudioSource audioSource;
    private Animator animator;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    public void PlayDoorSequence()
    {
        if (doorOpenSound != null)
            audioSource.PlayOneShot(doorOpenSound);

        if (animator != null)
            animator.Play("DoorOpen");

        Invoke("PlayDoorClose", delayBeforeClose);
    }

    private void PlayDoorClose()
    {
        if (doorCloseSound != null)
            audioSource.PlayOneShot(doorCloseSound);

        if (animator != null)
            animator.Play("DoorClose");

        // 🎬 Reset le lever
        if (shadowLever != null)
            shadowLever.ResetLever();
    }
}