using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_FarmerController2 : MonoBehaviour
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
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool playerIsClose; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int index = 0; 
    private string enemyWarriorMissionCompleted = "EnemyWarriorMissionCompleted";
    private string endDialogFarmer = "EndDialogFarmer";
    public GameObject farmerObject;
    public GameObject enemyWariorObject;
    public GameObject popupReward;
    public GameObject rewardAlert;
    
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
        bubleAsk.SetActive(false);
        popupReward.SetActive(false);
        rewardAlert.SetActive(false);
        nextButton.gameObject.SetActive(false);

        bool isComplete = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
        if(isComplete) {
            bubleAsk.SetActive(true);
            if (bubbleAskAnimator != null)
            {
                bubbleAskAnimator.SetTrigger("Show");
        }
        } 
        bool isCompleteDialog = PlayerPrefs.GetInt(endDialogFarmer, 0) == 1;
        if(isCompleteDialog) {
            farmerObject.SetActive(false);
            enemyWariorObject.SetActive(false);
        } else {
            farmerObject.SetActive(true);
            enemyWariorObject.SetActive(true);
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
        PlayerPrefs.SetInt(endDialogFarmer, 1); 
        farmerObject.SetActive(false);
        enemyWariorObject.SetActive(false);
        popupReward.SetActive(true);
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

    public void OnButtonClick() {
        rewardAlert.SetActive(true);
        popupReward.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
         bool isComplete = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (isComplete) {
            playerIsClose = true;
            talkButton.gameObject.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
         bool isComplete = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (isComplete) {
            playerIsClose = true;
            talkButton.gameObject.SetActive(false); 
            }
        }
    }
}
