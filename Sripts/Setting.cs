using UnityEngine;

public class Setting : MonoBehaviour
{
    private bool isAudioEnabled = true;
    private bool isVibrationEnabled = true;
    private AudioSource audioSource;
    private bool isMusicPlaying = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ToggleAudio()
    {
       
            if (isMusicPlaying)
            {
                // Stop the music
                audioSource.Pause();
                isMusicPlaying = false;
                Debug.Log("Music is paused");
            }
            else
            {
                // Resume the music
                audioSource.UnPause();
                isMusicPlaying = true;
                Debug.Log("Music is resumed");
            }
       
        isAudioEnabled = !isAudioEnabled;
    }

    public void ToggleVibration()
    {
        if (isVibrationEnabled)
        {
            // TODO: Implement logic to turn off vibration
            Debug.Log("Vibration is disabled");
        }
        else
        {
            // TODO: Implement logic to turn on vibration
            Debug.Log("Vibration is enabled");
        }

        isVibrationEnabled = !isVibrationEnabled;
    }

}
