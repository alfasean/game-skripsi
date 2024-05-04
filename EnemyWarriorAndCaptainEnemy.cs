using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EnemyWarriorAndCaptainEnemy : MonoBehaviour
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
        yield return StartCoroutine(TypeDialogue(wariorDialogue[wariorIndex], wariorDialogueText, wariorBubble, wariorBubbleAnimator, wariorDialoguePanel));
        wariorIndex = (wariorIndex + 1) % wariorDialogue.Length;

        yield return StartCoroutine(TypeDialogue(captainDialogue[captainIndex], captainDialogueText, captainBubble, captainBubbleAnimator, captainDialoguePanel));
        captainIndex = (captainIndex + 1) % captainDialogue.Length;

        yield return StartCoroutine(TypeDialogue(wariorDialogue[wariorIndex], wariorDialogueText, wariorBubble, wariorBubbleAnimator, wariorDialoguePanel));
        wariorIndex = (wariorIndex + 1) % wariorDialogue.Length;

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
        SceneManager.LoadScene(sceneToLoad);
    }
}
