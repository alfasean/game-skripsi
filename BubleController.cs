using UnityEngine;

public class BubbleController : MonoBehaviour
{
    public GameObject bubble; 
    public Animator npcAnimator; 
    private bool isPlayerClose; 

    void Start()
    {
        bubble.SetActive(false); 
    }

    void Update()
    {
        
        if (isPlayerClose && !bubble.activeSelf)
        {
            bubble.SetActive(true); 
            
            npcAnimator.SetBool("isTalking", true);
        }
        else if (!isPlayerClose && bubble.activeSelf)
        {
            bubble.SetActive(false); 
            
            npcAnimator.SetBool("isTalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isPlayerClose = false;
        }
    }
}
