using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Buble : MonoBehaviour
{
    public Sprite[] images; 
    public Image imageDisplay;
    public float switchTime = 2f; 

    private int currentIndex = 0;

    void Start()
    {
        imageDisplay.sprite = images[currentIndex];
        StartCoroutine(SwitchImage());
    }

    IEnumerator SwitchImage()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchTime);
            currentIndex = (currentIndex + 1) % images.Length;
            imageDisplay.sprite = images[currentIndex];
        }
    }
}
