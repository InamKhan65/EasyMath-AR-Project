using UnityEngine;

public class VectorySound : MonoBehaviour
{
    private AudioSource audioSource;
    private bool isMusicPlaying = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Stop(); // Start playing the background music when the scene loads
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop(); // Stop the background music
        isMusicPlaying = false;
    }

    public void PlayBackgroundMusic()
    {
        audioSource.Play(); // Start playing the background music again
        isMusicPlaying = true;
    }
}
