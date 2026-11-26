using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gallery : MonoBehaviour
{
    public Image imageDisplay; // Image component to display the images
    public Button nextButton; // Button to go to the next image
    public Button previousButton; // Button to go to the previous image
    public string imageFolderPath; // Path to the image folder inside the Resources folder

    private Sprite[] imageSprites; // Array to store the image sprites
    private int currentIndex = 0; // Index of the current image being displayed

    private void Start()
    {
        nextButton.onClick.AddListener(NextImage);
        previousButton.onClick.AddListener(PreviousImage);
        LoadImages();
    }

    // Load image sprites from the specified folder
    private void LoadImages()
    {
        Sprite[] loadedSprites = Resources.LoadAll<Sprite>(imageFolderPath);
        if (loadedSprites.Length > 0)
        {
            imageSprites = loadedSprites;
            DisplayImage(currentIndex);
        }
        else
        {
            Debug.LogError("No images found in the specified folder: " + imageFolderPath);
        }
    }

    // Display the image at the specified index
    private void DisplayImage(int index)
    {
        if (imageSprites != null && index >= 0 && index < imageSprites.Length)
        {
            imageDisplay.sprite = imageSprites[index];
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
