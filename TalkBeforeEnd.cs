using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class TalkBeforeEnd : MonoBehaviour
{
    public GameObject captainBubble;
    public Animator captainBubbleAnimator;
    public GameObject esaBubble;
    public Animator esaBubbleAnimator;
    public GameObject madarhikaBubble;
    public Animator madarhikaBubbleAnimator;
    public GameObject captainDialoguePanel;
    public Text captainDialogueText;
    public GameObject esaDialoguePanel;
    public Text esaDialogueText;
    public GameObject madarhikaDialoguePanel;
    public Text madarhikaDialogueText;
    public float wordSpeed;
    public AudioClip typingAudioClip;
    private AudioSource typingAudioSource;
    private bool isInteracting = false;
    private bool isTyping = false;
    private int captainIndex = 0;
    private int esaIndex = 0;
    private int madarhikaIndex = 0;
    public string sceneToLoad;
    private bool isDialogFinished = false;

    public string[] captainDialogue;
    public string[] esaDialogue;
    public string[] madarhikaDialogue;

    private void Start()
    {
        captainBubble.SetActive(false);
        esaBubble.SetActive(false);
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog()
    {
        yield return StartCoroutine(TypeDialogue(madarhikaDialogue[0], madarhikaDialogueText, madarhikaBubble, madarhikaBubbleAnimator, madarhikaDialoguePanel));

        yield return StartCoroutine(TypeDialogue(captainDialogue[0], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));

        yield return StartCoroutine(TypeDialogue(captainDialogue[1], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        
        yield return StartCoroutine(TypeDialogue(captainDialogue[2], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        
        yield return StartCoroutine(TypeDialogue(captainDialogue[3], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        

        yield return StartCoroutine(TypeDialogue(esaDialogue[0], esaDialogueText, esaBubble, esaBubbleAnimator, esaDialoguePanel));

        isDialogFinished = true;
        EndDialogue();
    }

    IEnumerator TypeDialogue(string dialog, Text dialogText, GameObject bubble, Animator bubbleAnimator, GameObject dialogPanel)
    {
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
    }

    private void EndDialogue()
    {
        isInteracting = false;
        captainDialogueText.text = "";
        esaDialogueText.text = "";
        madarhikaDialogueText.text = "";
        SceneManager.LoadScene(sceneToLoad);
    }
}
