using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WitchController : MonoBehaviour
{
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
    public Button nextButton2; 
    public Button nextButton3; 
    public Button nextButton4; 
    public Button nextDialogue2; 
    public Button buttonAction1;
    public Button buttonAction2;
    public Button giveOwlButton;
    public GameObject completeMission;
    public OwlController owlController;
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool playerIsClose; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int index = 0; 
    private int index2 = 0; 
    private bool owlGiven = false;
    private string owlMissionCompletedKey = "OwlMissionCompleted"; 

    public string[] dialogue;
    public string[] dialogue2;
    public GameObject rewardPanel;
    public GameObject alertReward;

    private void Start()
    {
        rewardPanel.SetActive(false);
        alertReward.SetActive(false);
        dialoguePanel.SetActive(false);
        buttonAction1.gameObject.SetActive(false);
        buttonAction2.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(false);
        nextButton3.gameObject.SetActive(false);
        nextButton4.gameObject.SetActive(false);
        nextDialogue2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        talkButton.onClick.AddListener(InteractWithNPC); 
        giveOwlButton.onClick.AddListener(GiveOwl);
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;
        bubble.SetActive(false);
        playerBubble.SetActive(false);
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        }
        
        if (PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1)
        {
            completeMission.gameObject.SetActive(true);
            bubleAsk.gameObject.SetActive(false);
        } else {
                completeMission.gameObject.SetActive(false);
        }
    }

    private void Update()
    {    
        if (playerIsClose && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC(); 
        }

        giveOwlButton.gameObject.SetActive(playerIsClose && owlController.owlTaken && !owlGiven && !isInteracting);
    }

    private void InteractWithNPC()
    {
        isInteracting = true;
        dialoguePanel.SetActive(true); 
        nextButton.gameObject.SetActive(true);
        StartCoroutine(TypeDialogue()); 
        talkButton.gameObject.SetActive(false);
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
    private void InteractWithNPC2()
    {
        isInteracting = true;
        dialoguePanel.SetActive(true); 
        nextDialogue2.gameObject.SetActive(true);
        StartCoroutine(TypeDialogue2()); 
        talkButton.gameObject.SetActive(false);
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

      IEnumerator TypeDialogue2()
    {
        isTyping = true;

        foreach (char letter in dialogue2[index2].ToCharArray())
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

        
        nextDialogue2.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
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

    private void EndDialogue2()
    {
        rewardPanel.SetActive(true);
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

     public void OnNextButtonClick()
    {
        dialogueText.text = "";
        index = 1;
        InteractWithNPC();
        buttonAction1.gameObject.SetActive(true);
        buttonAction2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(false);
        nextButton3.gameObject.SetActive(false);
        nextButton4.gameObject.SetActive(false);
    }

     public void OnButtonAction1Click()
    {
        dialogueText.text = "";
        index = 2;
        InteractWithNPC();
        buttonAction1.gameObject.SetActive(false);
        buttonAction2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(true);
        nextButton3.gameObject.SetActive(false);
        nextButton4.gameObject.SetActive(false);
    }

    public void OnNextButton2Click()
    {
        dialogueText.text = "";
        index = 3;
        InteractWithNPC();
        buttonAction1.gameObject.SetActive(false);
        buttonAction2.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(false);
        nextButton3.gameObject.SetActive(false);
        nextButton4.gameObject.SetActive(false);
    }

     public void OnButtonAction2Click()
    {
        dialogueText.text = "";
        index = 4;
        InteractWithNPC();
        buttonAction1.gameObject.SetActive(false);
        buttonAction2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(false);
        nextButton3.gameObject.SetActive(true);
        nextButton4.gameObject.SetActive(false);
    }

    public void OnNextButton3Click()
    {
        dialogueText.text = "";
        index = 6;
        InteractWithNPC();
        buttonAction1.gameObject.SetActive(false);
        buttonAction2.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(false);
        nextButton2.gameObject.SetActive(false);
        nextButton3.gameObject.SetActive(false);
        nextButton4.gameObject.SetActive(true);
    }
    public void OnNextButton4Click()
    {
        EndDialogue();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isComplete = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (isComplete) {
            playerIsClose = true;
            talkButton.gameObject.SetActive(false); 
            giveOwlButton.gameObject.SetActive(false);
            } else {
            playerIsClose = true;
            talkButton.gameObject.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isComplete = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (isComplete) {
            playerIsClose = true;
            talkButton.gameObject.SetActive(false); 
            giveOwlButton.gameObject.SetActive(false);
            } else {
            playerIsClose = false;
            talkButton.gameObject.SetActive(false); 
            }
        }
    }

    public void GiveOwl()
    {
        giveOwlButton.gameObject.SetActive(false);
        talkButton.gameObject.SetActive(false);
        // owlController.GiveOwlToWitch();
        InteractWithNPC2();
        
        StartCoroutine(TypeDialogue2());

        PlayerPrefs.Save();
    }

    public void OnNextDialogue2Click()
    {   
        dialogueText.text = "";
        
        if (index2 < dialogue2.Length - 1)
        {
            index2++; 
            StartCoroutine(TypeDialogue2()); 
        }
        else
        {
            
            owlGiven = true;
            PlayerPrefs.SetInt(owlMissionCompletedKey, 1);
            completeMission.gameObject.SetActive(true);
            EndDialogue2(); 
        }
    }

    public void OnGetClick() 
    {
        rewardPanel.SetActive(false);
        alertReward.SetActive(true);
    }
}
