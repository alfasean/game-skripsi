using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPC_WariorController : MonoBehaviour
{
    public MissionManager_FindHistoricalDocument missionManager; 
    public GameObject bubble; 
    public Animator bubleAnimator; 
    public GameObject playerBubble;
    public Animator playerBubleAnimator;
    public GameObject bubleAsk;
    public Animator bubbleAskAnimator;
    public GameObject dialoguePanel; 
    public GameObject keyStage2;
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
    public GameObject keyReward;
    public GameObject rewardInfo;
    private string wariorTakeKey = "wariorTakeKey";
    private string farmerTalk = "FarmerTalk";
    public GameObject interaksiLock;


    
    public string[] dialogue;

    private void Start()
    {
        dialoguePanel.SetActive(false);
        talkButton.gameObject.SetActive(false);
        keyStage2.SetActive(false);
        talkButton.onClick.AddListener(InteractWithNPC); 
        nextButton.onClick.AddListener(NextLine); 
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;
        bubble.SetActive(false);
        nextButton.gameObject.SetActive(false);
        playerBubble.SetActive(false);
        rewardInfo.SetActive(false);
        keyReward.SetActive(false);
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

    public void InteractWithNPC()
    {
        bool isClearMission = PlayerPrefs.GetInt(farmerTalk, 0) == 1;
        if (isClearMission)
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
        if(!isClearMission) {
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
        keyReward.SetActive(true);
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

    public void OnGetKeyClick()
    {
        if (missionManager != null)
        {
            missionManager.FoundDocument(); 
        }
        rewardInfo.SetActive(true);
        PlayerPrefs.SetInt(wariorTakeKey,1);
        keyReward.SetActive(false);
        keyStage2.SetActive(true);
    }

    public void ClosePopUp() {
        keyReward.SetActive(false);
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
