using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogAnimationGoa : MonoBehaviour
{
    public Image dialogImage; 
    public Sprite[] sprites; 
    private int currentSpriteIndex = 0; 
    private Coroutine spriteChangeCoroutine;
   

    void Start()
    {
    
        ShowSprite(currentSpriteIndex);
        spriteChangeCoroutine = StartCoroutine(ChangeSpriteAutomatically());
    }

    IEnumerator ChangeSpriteAutomatically()
    {
        while (true)
        {
            yield return new WaitForSeconds(6f); 
            ShowNextSprite();
        }
    }

    public void ShowSprite(int index)
    {
        if (index >= 0 && index < sprites.Length)
        {
            dialogImage.sprite = sprites[index];
        }
    }

    public void ShowNextSprite()
    {
        currentSpriteIndex++;
        if (currentSpriteIndex >= sprites.Length)
        {

            SceneManager.LoadScene("VideoAfterDialog");
        }
        else
        {
            ShowSprite(currentSpriteIndex);
        }
    }

    public void StopSpriteChangeCoroutine()
    {
        if (spriteChangeCoroutine != null)
        {
            StopCoroutine(spriteChangeCoroutine);
        }
    }
}
