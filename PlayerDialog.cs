using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerDialog : MonoBehaviour
{
    public GameObject dialogPanel;
    public Text dialogText;
    public Button nextButton;
    public float wordSpeed;
    public AudioSource typingAudioSource; 
    public AudioClip typingAudioClip; 
    private bool isTalking;
    private int index;
    public string[] dialogLines;
    private string playerName;

    void Start()
    {
        playerName = PlayerPrefs.GetString("PlayerName", "");

        if (!PlayerPrefs.HasKey(playerName + "_DialogShown"))
        {
            PlayerPrefs.SetInt(playerName + "_DialogShown", 1); 
            dialogPanel.SetActive(true);
            nextButton.gameObject.SetActive(false);
            StartCoroutine(TypeDialog());
            isTalking = true;
        }
        else
        {
            dialogPanel.SetActive(false);
            nextButton.gameObject.SetActive(false);
            isTalking = false;
        }
    }

    IEnumerator TypeDialog()
    {
        foreach (char letter in dialogLines[index].ToCharArray())
        {
            dialogText.text += letter;
            typingAudioSource.clip = typingAudioClip;
            typingAudioSource.Play();
            yield return new WaitForSeconds(wordSpeed);
        }

        typingAudioSource.Stop();
        nextButton.gameObject.SetActive(true);
         

        while (!Input.GetKeyDown(KeyCode.Space))
        {
            yield return null;
        }

        NextLine();
    }

    public void NextLine()
    {
        index++;
        if (index < dialogLines.Length)
        {
            dialogText.text = ""; 
            StartCoroutine(TypeDialog()); 
            nextButton.gameObject.SetActive(false); 
        }
        else
        {
            EndDialog();
        }
    }

    public void EndDialog()
    {
        dialogPanel.SetActive(false);
        nextButton.gameObject.SetActive(false);
        isTalking = false;
        index = 0; 
    }
}
