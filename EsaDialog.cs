using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class EsaDialog : MonoBehaviour
{
    public GameObject esaBubble;
    public Animator esaBubbleAnimator;
    public GameObject esaDialoguePanel;
    public Text esaDialogueText;
    public float wordSpeed;
    public AudioClip typingAudioClip;
    private AudioSource typingAudioSource;
    private bool isTyping = false;
    private int esaIndex = 0;
    public string sceneToLoad;
    private bool isDialogFinished = false;

    public string[] esaDialogue;

    private void Start()
    {
        esaBubble.SetActive(false);
        typingAudioSource = gameObject.AddComponent<AudioSource>();
        StartCoroutine(StartDialog());
    }

    IEnumerator StartDialog()
    {
        yield return StartCoroutine(TypeDialogue(esaDialogue[0], esaDialogueText, esaBubble, esaBubbleAnimator, esaDialoguePanel));

        yield return StartCoroutine(TypeDialogue(esaDialogue[1], esaDialogueText, esaBubble, esaBubbleAnimator, esaDialoguePanel));

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
        SceneManager.LoadScene(sceneToLoad);
    }
}
