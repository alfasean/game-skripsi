using UnityEngine;
using UnityEngine.UI;

public class BookHistoricalController : MonoBehaviour
{
    public Button takeButton;
    public GameObject objectBook;
    public GameObject bookImage; 
    public GameObject popup;
    public GameObject rewardInfo;
    public MissionManager_FindBook missionManager; 
    public AudioSource findBookAudioSource;
    public AudioClip findBookAudioClip;
    private string findBookMissionKey = "FindBookMissionCompleted";
    private string kaptenDialog = "kaptenDialog";

    private bool playerIsClose;

    void Start()
    {
        popup.gameObject.SetActive(false);
        rewardInfo.gameObject.SetActive(false);
        bool missionCompleted = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
        if (missionCompleted)
        {
            objectBook.SetActive(false);
        } 
    }

    void Update()
    {
        bool isClearMission = PlayerPrefs.GetInt(kaptenDialog, 0) == 1;
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

    public void OnGetButtonClick()
    {
        findBookAudioSource.clip = findBookAudioClip;
        findBookAudioSource.Play();
        popup.gameObject.SetActive(false);
        Debug.Log("Player mengambil buku sejarah");
        objectBook.gameObject.SetActive(false);
        takeButton.gameObject.SetActive(false);
        PlayerPrefs.SetInt(findBookMissionKey, 1); 
        missionManager.FindBook(); 
        rewardInfo.gameObject.SetActive(true);
        bookImage.SetActive(true);
    }

    public void ClosePopUp()
    {
        popup.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
        }
    }
}
