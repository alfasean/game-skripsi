using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Button talkButton;
    public Button nextButton;
    public Button introductionButton;
    public Button whatPlaceButton;
    public Button howButton;
    public Button noButton;
    public Button thanksButton;
    public GameObject bubble;
    public Animator bubleAnimator;
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject bubleAsk;
    public Animator bubbleAskAnimator;
    public float wordSpeed;
    private bool playerIsClose;
    private bool isInteracting;
    private int index;
    public string[] dialogue;
    public MissionManager_InteractNPC missionManager;
    private bool isTyping = false;
    public AudioSource typingAudioSource;
    public AudioClip typingAudioClip;
    private string kadesDialog = "kadesDialog";

    void Start()
    {
        dialoguePanel.SetActive(false);
        talkButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        thanksButton.gameObject.SetActive(false);
        isInteracting = false;
        bubble.SetActive(false);
        playerBubble.SetActive(false);
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        }
    }

    void Update()
    {
        if (playerIsClose && !isInteracting)
        {
            talkButton.gameObject.SetActive(true);
        }
    }

    public void StartDialogue()
    {
        dialoguePanel.SetActive(true);
        StartCoroutine(Typing());
        talkButton.gameObject.SetActive(false);
        isInteracting = true;
        bubleAsk.SetActive(false);
        noButton.gameObject.SetActive(false);
        howButton.gameObject.SetActive(true);
        whatPlaceButton.gameObject.SetActive(true);
        thanksButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);

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

    IEnumerator Typing()
    {
        isTyping = true;

        foreach (char letter in dialogue[index].ToCharArray())
        {
            typingAudioSource.clip = typingAudioClip;
            typingAudioSource.Play();
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
        typingAudioSource.Stop();

        // nextButton.gameObject.SetActive(true);

        if (index >= dialogue.Length - 1 && !isTyping && missionManager != null)
        {
            missionManager.InteractWithNPC();
        }
    }

    // public void NextLine()
    // {
    //     if (!isTyping)
    //     {
    //         if (index < dialogue.Length - 1)
    //         {
    //             index++;
    //             dialogueText.text = "";
    //             StartCoroutine(Typing());
    //             nextButton.gameObject.SetActive(false);
    //         }
    //         else
    //         {
    //             ZeroText();
    //         }
    //     }
    // }

    public void OnTalkButtonClick()
    {
        StartDialogue();
    }

    public void OnNextButtonClick()
    {
        // NextLine();
        ZeroText();
        index = 4;
        StartDialogue();
        noButton.gameObject.SetActive(false);
        howButton.gameObject.SetActive(true);
        whatPlaceButton.gameObject.SetActive(false);
        thanksButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        PlayerPrefs.SetInt(kadesDialog, 1);
    }

    public void OnIntroductionButtonClick()
    {
        ZeroText();
        index = 1;
        StartDialogue();
        noButton.gameObject.SetActive(true);
        howButton.gameObject.SetActive(true);
        whatPlaceButton.gameObject.SetActive(true);
        thanksButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(false);
    }

    public void OnWhatsPlaceButtonClick()
    {
        ZeroText();
        index = 3;
        StartDialogue();
        noButton.gameObject.SetActive(false);
        howButton.gameObject.SetActive(false);
        whatPlaceButton.gameObject.SetActive(false);
        thanksButton.gameObject.SetActive(false);
        introductionButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
    }

    public void OnHowButtonClick()
    {
        ZeroText();
        index = 2;
        StartDialogue();
        noButton.gameObject.SetActive(false);
        howButton.gameObject.SetActive(false);
        whatPlaceButton.gameObject.SetActive(false);
        thanksButton.gameObject.SetActive(true);
        introductionButton.gameObject.SetActive(false);
    }   
    
    public void OnThanksButtonClick()
    {
       dialoguePanel.SetActive(false);
       bubble.SetActive(false);
    }   

    public void OnNoButtonClick()
    {
       dialoguePanel.SetActive(false);
       bubble.SetActive(false);
    }   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;

            if (!isInteracting && talkButton != null)
            {
                talkButton.gameObject.SetActive(false);
            }

            ZeroText();
        }
    }
}
