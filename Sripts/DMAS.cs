using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DMAS : MonoBehaviour
{
    public Image imageDisplay; // Image component to display the images
    public Button nextButton; // Button to go to the next image
    public Button previousButton; // Button to go to the previous image
    public string imageFolderPath; // Path to the image folder inside the Resources folder

    private Sprite[] imageSprites; // Array to store the image sprites
    private AudioClip[] imageSounds; // Array to store the image sounds
    private int currentIndex = 0; // Index of the current image being displayed

    private void Start()
    {
        nextButton.onClick.AddListener(NextImage);
        previousButton.onClick.AddListener(PreviousImage);
        LoadImages();
    }

    // Load image sprites and sounds from the specified folder
    private void LoadImages()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(imageFolderPath);
        AudioClip[] loadedSounds = new AudioClip[loadedSprites.Length];

        for (int i = 0; i < loadedSprites.Length; i++)
        {
            string soundPath = imageFolderPath + "/" + loadedSprites[i].name;
            loadedSounds[i] = Resources.Load<AudioClip>(soundPath);
        }

        if (loadedSprites.Length > 0)
        {
            imageSprites = loadedSprites;
            imageSounds = loadedSounds;
            DisplayImage(currentIndex);
        }
        else
        {
            Debug.LogError("No images found in the specified folder: " + imageFolderPath);
        }
    }

    // Display the image at the specified index and play the corresponding sound
    private void DisplayImage(int index)
    {
        if (imageSprites != null && index >= 0 && index < imageSprites.Length)
        {
            imageDisplay.sprite = imageSprites[index];
            PlaySound(index);
        }
    }

    // Play the sound for the current image
    private void PlaySound(int index)
    {
        if (imageSounds != null && index >= 0 && index < imageSounds.Length)
        {
            // Assuming you have an AudioSource component attached to the same GameObject
            AudioSource audioSource = GetComponent<AudioSource>();
            audioSource.clip = imageSounds[index];
            audioSource.Play();
        }
    }

    // Go to the next image
    public void NextImage()
    {
        currentIndex++;
        if (currentIndex >= imageSprites.Length)
        {
            currentIndex = 0;
        }

        DisplayImage(currentIndex);
    }

    // Go to the previous image
    public void PreviousImage()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = imageSprites.Length - 1;
        }

        DisplayImage(currentIndex);
    }
}
