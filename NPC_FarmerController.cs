using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_FarmerController : MonoBehaviour
{
    public MissionManager_FindHistoricalDocument missionManager; 
    public GameObject bubble; 
    public Animator bubleAnimator; 
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject bubleAsk;
    public Animator bubbleAskAnimator;
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

    private string kadesDialog = "kadesDialog";
    private string findBookMissionKey = "FindBookMissionCompleted";
    private string farmerTalk = "FarmerTalk";
    public GameObject interaksiLock;

    
    public string[] dialogue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        talkButton.gameObject.SetActive(false);
        talkButton.onClick.AddListener(InteractWithNPC); 
        nextButton.onClick.AddListener(NextLine);  
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        nextButton.gameObject.SetActive(false);
        typingAudioSource.loop = false;
        bubble.SetActive(false);
        playerBubble.SetActive(false);
        interaksiLock.SetActive(false);
        
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        } 
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
        bool isClearMissionKades = PlayerPrefs.GetInt(kadesDialog, 0) == 1;
        bool isClearMissionBook = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
        if(isClearMissionBook && isClearMissionKades)
        {
        isInteracting = true;
        talkButton.gameObject.SetActive(false);
        dialoguePanel.SetActive(true); 
        StartCoroutine(TypeDialogue()); 
        bubleAsk.SetActive(false);

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
        if (!isClearMissionBook && !isClearMissionKades) {
            interaksiLock.SetActive(true);
            Invoke("DisableInteraksiLock", 5f);
            return;
        }
    }
    private void DisableInteraksiLock()
    {
        interaksiLock.SetActive(false);
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
        nextButton.gameObject.SetActive(true);
    }

    
    private void NextLine()
    {
        
        dialogueText.text = "";
        
        if (index < dialogue.Length - 1)
        {
            index++; 
            StartCoroutine(TypeDialogue()); 
            nextButton.gameObject.SetActive(false);
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
        PlayerPrefs.SetInt(farmerTalk, 1);
        if (missionManager != null)
        {
            missionManager.FoundDocument(); 
        }
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
