using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EnemyCaptainController : MonoBehaviour
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
    public GameObject playerBubble;
    public Animator playerBubbleAnimator;
    public GameObject bubbleAsk;
    public Animator bubbleAskAnimator;

    private GameObject player;
    private bool playerInRange;
    private bool isInteracting = false;
    private int index = 0;
    private AudioSource typingAudioSource;

    private string EnemyCaptainMissionCompleted = "EnemyCaptainMissionCompleted";

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        talkButton.SetActive(false);
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(NextLine);
        bubble.SetActive(false);
        playerBubble.SetActive(false);
        if (bubbleAskAnimator != null)
        {
            bubbleAskAnimator.SetTrigger("Show");
        }
        
        if (PlayerPrefs.GetInt(EnemyCaptainMissionCompleted, 0) == 1)
        {
            completeMission.gameObject.SetActive(true);
            bubbleAsk.gameObject.SetActive(false);
        } else {
                completeMission.gameObject.SetActive(false);
        }

        typingAudioSource = gameObject.AddComponent<AudioSource>();
        typingAudioSource.playOnAwake = false;
        typingAudioSource.loop = false;
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
        playerBubble.SetActive(true);

        if (bubbleAnimator != null)
        {
            bubbleAnimator.SetBool("isTalking", true);
        }
        if (playerBubbleAnimator != null)
        {
            playerBubbleAnimator.SetBool("isTalking", true);
        }

        if (bubbleAsk != null)
        {
            bubbleAsk.SetActive(false);
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
    }

    void NextLine()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StartCoroutine(TypeDialogue());
        }
        else {
            PlayerPrefs.SetInt(EnemyCaptainMissionCompleted, 1); 
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
            playerInRange = true;
            talkButton.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            talkButton.gameObject.SetActive(false);
            dialoguePanel.SetActive(false);

            // Deactivate all bubbles and their animations
            if (bubble != null)
            {
                bubble.SetActive(false);
            }
            if (playerBubble != null)
            {
                playerBubble.SetActive(false);
            }
            if (bubbleAnimator != null)
            {
                bubbleAnimator.SetBool("isTalking", false);
            }
            if (playerBubbleAnimator != null)
            {
                playerBubbleAnimator.SetBool("isTalking", false);
            }

            // Activate bubble ask again
            if (bubbleAsk != null)
            {
                bubbleAsk.SetActive(true);
            }
        }
    }
}
