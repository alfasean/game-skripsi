using UnityEngine;

public class DialogController : MonoBehaviour
{
    public DialogAnimation dialogAnimation; 

    public void OnNextButtonClicked()
    {
        dialogAnimation.ShowNextSprite();
    }
}