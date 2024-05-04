using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class WariorAndCaptainTalk : MonoBehaviour
{
    public GameObject wariorBubble; 
    public Animator wariorBubbleAnimator; 
    public GameObject captainBubble;
    public Animator captainBubbleAnimator;
    public GameObject wariorDialoguePanel; 
    public Text wariorDialogueText;  
    public GameObject captainDialoguePanel; 
    public Text captainDialogueText;  
    public float wordSpeed; 
    public AudioClip typingAudioClip; 
    private AudioSource typingAudioSource; 
    private bool isInteracting = false; 
    private bool isTyping = false; 
    private int wariorIndex = 0; 
    private int captainIndex = 0; 
    private bool isWariorSpeaking = true; 
    public string sceneToLoad;
    private bool isDialogFinished = false; 

    public string[] wariorDialogue;
    public string[] captainDialogue;

    private void Start()
    {
        captainBubble.SetActive(false);
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog()
    {
        while (!isDialogFinished) 
        {
            string currentDialog;
            if (isWariorSpeaking)
            {
                currentDialog = wariorDialogue[wariorIndex];
                wariorIndex = (wariorIndex + 1) % wariorDialogue.Length;
            }
            else
            {
                currentDialog = captainDialogue[captainIndex];
                captainIndex = (captainIndex + 1) % captainDialogue.Length;
            }

            yield return StartCoroutine(TypeDialogue(currentDialog, isWariorSpeaking));

            isWariorSpeaking = !isWariorSpeaking; 
        }

        
        EndDialogue();
    }

    IEnumerator TypeDialogue(string dialog, bool isWarior)
    {
        GameObject bubble = isWarior ? wariorBubble : captainBubble;
        Animator bubbleAnimator = isWarior ? wariorBubbleAnimator : captainBubbleAnimator;
        Text dialogText = isWarior ? wariorDialogueText : captainDialogueText;
        GameObject dialogPanel = isWarior ? wariorDialoguePanel : captainDialoguePanel; 

        isTyping = true;
        bubble.SetActive(true); 
        bubbleAnimator.SetBool("isTalking", true); 

        dialogPanel.SetActive(true); 

        dialogText.text = ""; 

        foreach (char letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            if (typingAudioClip != null)
            {
                typingAudioSource.clip = typingAudioClip;
                typingAudioSource.Play(); 
            }
            yield return new WaitForSeconds(wordSpeed);
        }

        isTyping = false;
        typingAudioSource.Stop();

        
        bubble.SetActive(false);
        bubbleAnimator.SetBool("isTalking", false);

        dialogPanel.SetActive(false); 

        
        yield return new WaitForSeconds(2f);

        
        if ((isWarior && wariorIndex == 0) || (!isWarior && captainIndex == 0))
        {
            isDialogFinished = true;
        }
    }

    private void EndDialogue()
    {
        isInteracting = false; 
        wariorIndex = 0; 
        captainIndex = 0;
        wariorDialogueText.text = ""; 
        captainDialogueText.text = ""; 
        SceneManager.LoadScene(sceneToLoad); 
    }
}
