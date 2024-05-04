using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyWarriorController : MonoBehaviour
{
    public float detectionRange = 3f;
    public GameObject talkButton;
    public GameObject dialoguePanel;
    public Text dialogueText;
    public Button nextButton;
    public string[] dialogue;
    public float wordSpeed; 
    public AudioClip typingAudioClip;
    public string fightSceneName;
    public GameObject completeMission;
    public GameObject bubble; 
    public Animator bubbleAnimator; 
    public GameObject bubbleAsk; 
    public Animator bubbleAskAnimator; 
    public GameObject playerBubble; 
    public Animator playerBubbleAnimator; 

    private GameObject player;
    private bool playerInRange;
    private bool isInteracting = false;
    private int index = 0;
    private AudioSource typingAudioSource;

    private string enemyWarriorMissionCompleted = "EnemyWarriorMissionCompleted";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        talkButton.SetActive(false);
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
        nextButton.gameObject.SetActive(false);

        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        }

        if (PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1)
        {
            completeMission.SetActive(true);
            bubbleAsk.gameObject.SetActive(false);
        } else {
            completeMission.SetActive(false);
        }

        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;

        bubble.SetActive(false);
        playerBubble.SetActive(false);
    }

    void Update()
    {
        if (playerInRange && !isInteracting)
        {
            if (Vector2.Distance(transform.position, player.transform.position) <= detectionRange)
            {
                // talkButton.SetActive(true);
            }
            else
            {
                talkButton.SetActive(false);
                dialoguePanel.SetActive(false);
            }
        }
    }

    public void StartDialogue()
    {
        talkButton.SetActive(false);
        dialoguePanel.SetActive(true);
        StartCoroutine(TypeDialogue());
        isInteracting = true;

        bubble.SetActive(true);
        bubbleAsk.SetActive(false);
        playerBubble.SetActive(true);

        if (bubbleAnimator != null)
        {
            bubbleAnimator.SetBool("isTalking", true);
        }
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetBool("isTalking", true);
        }
        if (playerBubbleAnimator != null)
        {
            playerBubbleAnimator.SetBool("isTalking", true);
        }
    }

    IEnumerator TypeDialogue()
    {
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
        typingAudioSource.Stop(); 
        isInteracting = false;
        nextButton.gameObject.SetActive(true);
    }

    void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeDialogue());
            nextButton.gameObject.SetActive(false);
        }
        else {
            PlayerPrefs.SetInt(enemyWarriorMissionCompleted, 1); 
            PlayerPrefs.Save();
            completeMission.gameObject.SetActive(true);
            StartFight();
        }
    }

    void StartFight()
    {
        SceneManager.LoadScene(fightSceneName);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            bool isComplete = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
            if (!isComplete) {
            playerInRange = true;
            talkButton.gameObject.SetActive(true);
            } 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            talkButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);

            if (bubble != null)
            {
                bubble.SetActive(false);
            }
            if (bubbleAnimator != null)
            {
                bubbleAnimator.SetBool("isTalking", false);
            }

            if (bubbleAsk != null)
            {
                bubbleAsk.SetActive(false);
            }
            if (bubbleAskAnimator != null)
            {
                bubbleAskAnimator.SetBool("isTalking", false);
            }

            if (playerBubble != null)
            {
                playerBubble.SetActive(false);
            }
            if (playerBubbleAnimator != null)
            {
                playerBubbleAnimator.SetBool("isTalking", false);
            }
        }
    }
}
