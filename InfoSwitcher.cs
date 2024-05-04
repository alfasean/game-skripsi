using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InfoSwitcher : MonoBehaviour
{
    public Sprite[] images; 
    public Image displayImage; 
    public float fadeDuration = 1f; 
    public float displayDuration = 8f; 

    private int currentIndex = 0; 
    private Coroutine switchRoutine; 

    void Start()
    {
        
        if (images.Length == 0 || displayImage == null)
        {
            Debug.LogWarning("Array gambar kosong atau komponen Image tidak ditetapkan!");
            return;
        }

        
        switchRoutine = StartCoroutine(SwitchImageRoutine());
    }

    IEnumerator SwitchImageRoutine()
    {
        while (true)
        {
            
            yield return FadeImage(false);

            
            yield return new WaitForSeconds(fadeDuration);

            
            currentIndex = (currentIndex + 1) % images.Length;
            displayImage.sprite = images[currentIndex];

            
            yield return FadeImage(true);

            
            yield return new WaitForSeconds(displayDuration);
        }
    }

    IEnumerator FadeImage(bool fadeIn)
    {
        
        float targetAlpha = fadeIn ? 1f : 0f;
        Color startColor = displayImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);

        
        float startTime = Time.time;
        float elapsedTime = 0f;

        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            float t = Mathf.Clamp01(elapsedTime / fadeDuration);
            displayImage.color = Color.Lerp(startColor, targetColor, t);
            yield return null;
        }

        
        displayImage.color = targetColor;
    }

    
    public void StopImageSwitching()
    {
        if (switchRoutine != null)
        {
            StopCoroutine(switchRoutine);
        }
    }
}
