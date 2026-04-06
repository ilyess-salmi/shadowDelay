using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip jumpSound;
    public AudioClip collectSound; 

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    public void PlayCollect() 
    {
        audioSource.PlayOneShot(collectSound);
    }
}