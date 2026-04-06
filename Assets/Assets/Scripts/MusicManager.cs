using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip musicClip;
    public float volume = 0.1f; // change dans Inspector

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        AudioSource audio = gameObject.AddComponent<AudioSource>();
        audio.clip = musicClip;
        audio.loop = true;
        audio.volume = volume;
        audio.Play();
    }
}