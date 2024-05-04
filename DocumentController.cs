using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DocumentController : MonoBehaviour
{
    public Button takeButton; 
    public GameObject popup;
    public GameObject rewardInfo;
    public GameObject documentObject;
    public GameObject mapsImage;
    private bool playerIsClose;
    public AudioSource takeDocumentAudioSource;
    public AudioClip takeDocumentAudioClip;
    private string documentMissionCompletedKey = "DocumentMissionCompleted"; 
    private string findBookMissionKey = "FindBookMissionCompleted";

    void Start()
    {
        takeButton.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
        rewardInfo.gameObject.SetActive(false);
        takeButton.onClick.AddListener(OnTakeButtonClick);
        bool missionCompleted = PlayerPrefs.GetInt(documentMissionCompletedKey, 0) == 1;
        if (missionCompleted)
        {
            documentObject.SetActive(false);
        }
    }

    void Update()
    {
        bool isClearMission = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
            if (isClearMission)
            {
               if (playerIsClose)
                {
                    takeButton.gameObject.SetActive(true);
                }
                else
                {
                    takeButton.gameObject.SetActive(false);
                }
            }
    }

    public void OnTakeButtonClick() 
    {
        popup.gameObject.SetActive(true);
    }
    
    public void OnGetClick()
    {
        takeDocumentAudioSource.clip = takeDocumentAudioClip;
        takeDocumentAudioSource.Play();
        Debug.Log("Player mengambil benda bersejarah");
        documentObject.gameObject.SetActive(false);
        takeButton.gameObject.SetActive(false);
        mapsImage.SetActive(true);
        PlayerPrefs.SetInt(documentMissionCompletedKey, 1); 
        PlayerPrefs.Save(); 
        FindObjectOfType<MissionManager_FindHistoricalDocument>().FoundDocument();
        popup.gameObject.SetActive(false);
        rewardInfo.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    public void ClosePopUp()
    {
        popup.gameObject.SetActive(false);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}