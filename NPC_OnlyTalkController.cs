using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_OnlyTalkController : MonoBehaviour
{
    public GameObject bubble; 
    public Animator bubleAnimator; 
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject dialoguePanel; 
    public Text dialogueText; 
    public Button talkButton; 
    public Button nextButton; 
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool playerIsClose; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int index = 0; 

    
    public string[] dialogue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        talkButton.onClick.AddListener(InteractWithNPC); 
        nextButton.onClick.AddListener(NextLine); 
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;
        bubble.SetActive(false);
        playerBubble.SetActive(false);
    }

    private void Update()
    {
        
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC(); 
        }
    }

    private void InteractWithNPC()
    {
        
        isInteracting = true;
        dialoguePanel.SetActive(true); 
        StartCoroutine(TypeDialogue()); 
        talkButton.gameObject.SetActive(false);

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
        dialoguePanel.SetActive(false); 
        isInteracting = false; 
        index = 0; 
        dialogueText.text = ""; 
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


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
            talkButton.gameObject.SetActive(true); 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            talkButton.gameObject.SetActive(false); 
        }
    }
}