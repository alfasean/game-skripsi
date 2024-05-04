using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_KaptenController : MonoBehaviour
{
    // public MissionManager_FindHistoricalDocument missionManager; 
    public GameObject bubble;
    public Animator bubleAnimator;
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject bubleAsk;
    public Animator bubbleAskAnimator;
    public GameObject dialoguePanel; 
    public Text dialogueText; 
    public Button talkButton; 
    // public Button nextButton; 
    public Button introductionButton;
    public Button findBookButton;
    public Button promiseButton;
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool playerIsClose; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int index = 0; 
    private string kadesDialog = "kadesDialog";
    public GameObject interaksiLcok;
    private string kaptenDialog = "kaptenDialog";


    
    public string[] dialogue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        talkButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(true);
        findBookButton.gameObject.SetActive(true);
        promiseButton.gameObject.SetActive(false);
        talkButton.onClick.AddListener(InteractWithNPC); 
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;
        bubble.SetActive(false);
        interaksiLcok.SetActive(false);
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        }
    }

    private void Update()
    {
        
        // if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        // {
        //     InteractWithNPC(); 
        // }
    }

    private void InteractWithNPC()
    {
        bool isClearMission = PlayerPrefs.GetInt(kadesDialog, 0) == 1;
        if (isClearMission)
        {
            isInteracting = true;
            talkButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(true);
            introductionButton.gameObject.SetActive(true);
            findBookButton.gameObject.SetActive(true);
            promiseButton.gameObject.SetActive(false);
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
        if (!isClearMission) {
            interaksiLcok.SetActive(true);
            Invoke("DisableInteraksiLock", 2f);
            return;
        }
    }

    private void DisableInteraksiLock()
    {
        interaksiLcok.SetActive(false);
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

     public void ZeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
        isInteracting = false;

        if (bubble != null)
        {
            bubble.SetActive(false);
        }

        if (bubleAnimator != null)
        {
            bubleAnimator.SetBool("isTalking", false);
        }
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

    public void OnIntroductionButtonClick()
    {
        ZeroText();
        index = 1;
        InteractWithNPC();
        findBookButton.gameObject.SetActive(true);
        introductionButton.gameObject.SetActive(false);
        promiseButton.gameObject.SetActive(false);
    }

    public void OnFindBookButtonClick()
    {
        ZeroText();
        index = 2;
        InteractWithNPC();
        findBookButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(false);
        promiseButton.gameObject.SetActive(true);
    }

    public void OnPromisekButtonClick()
    {
        EndDialogue();
    }

    
    private void EndDialogue()
    {
        dialoguePanel.SetActive(false); 
        isInteracting = false; 
        index = 0; 
        dialogueText.text = ""; 
        PlayerPrefs.SetInt(kaptenDialog, 1);
        // if (missionManager != null)
        // {
        //     missionManager.FoundDocument(); 
        // }
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
