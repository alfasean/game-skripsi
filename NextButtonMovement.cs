using UnityEngine;
using UnityEngine.UI;

public class NextButtonAnimation : MonoBehaviour
{
    public Animation nextButtonAnimation;

    void Start()
    {
        nextButtonAnimation = GetComponent<Animation>();
    }

    public void PlayAnimation()
    {
        if (nextButtonAnimation != null)
        {
            nextButtonAnimation.Play();
        }
    }
}
