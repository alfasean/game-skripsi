using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class KadesOnlyTalk : MonoBehaviour
{
    public GameObject bubble; 
    public Animator bubleAnimator; 
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject dialoguePanel; 
    public Text dialogueText;  
    public Button nextButton; 
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool playerIsClose; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int index = 0; 
    private string sceneToLoad = "LoadingIntoStage2";

    
    public string[] dialogue;

    private void Start()
    {
        // dialoguePanel.SetActive(true); 
        nextButton.onClick.AddListener(NextLine); 
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        InteractWithNPC();
    }

    private void InteractWithNPC()
    {
        
        isInteracting = true;
        dialoguePanel.SetActive(true); 
        StartCoroutine(TypeDialogue()); 

        if (bubble != null)
        {
            bubble.SetActive(true);
        }
        if (playerBubble != null)
        {
            playerBubble.SetActive(true);
        }

        if (bubleAnimator != null)
        {
            bubleAnimator.SetBool("isTalking", true);
        }
        if (playerBubleAnimator != null)
        {
            playerBubleAnimator.SetBool("isTalking", true);
        }
    }
    
    IEnumerator TypeDialogue()
    {
        isTyping = true;

        foreach (char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            if (typingAudioClip != null)
            {
                typingAudioSource.clip = typingAudioClip;
                typingAudioSource.Play(); 
            }
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
        typingAudioSource.Stop();
    }

    
    private void NextLine()
    {
        
        dialogueText.text = "";
        
        if (index < dialogue.Length - 1)
        {
            index++; 
            StartCoroutine(TypeDialogue()); 
        }
        else
        {
            EndDialogue(); 
        }
    }

    
    private void EndDialogue()
    {
        isInteracting = false; 
        index = 0; 
        dialogueText.text = ""; 
        SceneManager.LoadScene(sceneToLoad);
        if (bubble != null)
        {
            bubble.SetActive(false);
        }

        if (playerBubble != null)
        {
            playerBubble.SetActive(false);
        }

        if (bubleAnimator != null)
        {
            bubleAnimator.SetBool("isTalking", false);
        }

        if (playerBubleAnimator != null)
        {
            playerBubleAnimator.SetBool("isTalking", false);
        }
    }
}
