using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyWariorAndCaptainTalk : MonoBehaviour
{
    public GameObject wariorBubble;
    public Animator wariorBubbleAnimator;
    public GameObject captainBubble;
    public Animator captainBubbleAnimator;
    public GameObject esaBubble;
    public Animator esaBubbleAnimator;
    public GameObject madarhikaBubble;
    public Animator madarhikaBubbleAnimator;
    public GameObject wariorDialoguePanel;
    public Text wariorDialogueText;
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
    private int wariorIndex = 0;
    private int captainIndex = 0;
    private int esaIndex = 0;
    private int madarhikaIndex = 0;
    private bool isWariorSpeaking = true;
    public string sceneToLoad;
    private bool isDialogFinished = false;

    public string[] wariorDialogue;
    public string[] captainDialogue;
    public string[] esaDialogue;
    public string[] madarhikaDialogue;

    private void Start()
    {
        captainBubble.SetActive(false);
        esaBubble.SetActive(false);
        madarhikaBubble.SetActive(false);
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog()
    {
        yield return StartCoroutine(TypeDialogue(madarhikaDialogue[madarhikaIndex], madarhikaDialogueText, madarhikaBubble, madarhikaBubbleAnimator, madarhikaDialoguePanel));
        madarhikaIndex = (madarhikaIndex + 1) % madarhikaDialogue.Length;

        yield return StartCoroutine(TypeDialogue(wariorDialogue[wariorIndex], wariorDialogueText, wariorBubble, wariorBubbleAnimator, wariorDialoguePanel));
        wariorIndex = (wariorIndex + 1) % wariorDialogue.Length;

        yield return StartCoroutine(TypeDialogue(captainDialogue[captainIndex], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        captainIndex = (captainIndex + 1) % captainDialogue.Length;

        yield return StartCoroutine(TypeDialogue(madarhikaDialogue[madarhikaIndex], madarhikaDialogueText, madarhikaBubble, madarhikaBubbleAnimator, madarhikaDialoguePanel));
        madarhikaIndex = (madarhikaIndex + 1) % madarhikaDialogue.Length;

        yield return StartCoroutine(TypeDialogue(esaDialogue[esaIndex], esaDialogueText, esaBubble, esaBubbleAnimator, esaDialoguePanel));
        esaIndex = (esaIndex + 1) % esaDialogue.Length;

        yield return StartCoroutine(TypeDialogue(esaDialogue[esaIndex], esaDialogueText, esaBubble, esaBubbleAnimator, esaDialoguePanel));
        esaIndex = (esaIndex + 1) % esaDialogue.Length;

        yield return StartCoroutine(TypeDialogue(captainDialogue[captainIndex], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        captainIndex = (captainIndex + 1) % captainDialogue.Length;

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
        wariorDialogueText.text = "";
        captainDialogueText.text = "";
        esaDialogueText.text = "";
        madarhikaDialogueText.text = "";
        SceneManager.LoadScene(sceneToLoad);
    }
}
