using UnityEngine;
using UnityEngine.UI;

public class KeyController : MonoBehaviour
{
    public Button takeButton;
    public GameObject keyObject;
    public GameObject rewardInfo;
    public GameObject alertKey;
    private bool playerIsClose;
    public AudioSource takeKeyAudioSource;
    public AudioClip takeKeyAudioClip;
    private string keyMissionCompletedKey = "KeyMissionCompleted";
    private string findBookMissionKey = "FindBookMissionCompleted";
    private string documentMissionKey = "DocumentCount";

    void Start()
    {
        takeButton.gameObject.SetActive(false);
        rewardInfo.SetActive(false);
        alertKey.SetActive(false);
        bool missionCompleted = PlayerPrefs.GetInt(keyMissionCompletedKey, 0) == 1;
        if (missionCompleted)
        {
            keyObject.SetActive(false);
        }
    }

    void Update()
    {
        if (playerIsClose && keyObject.activeSelf) 
        {
            takeButton.gameObject.SetActive(true);
        }
        else
        {
            takeButton.gameObject.SetActive(false);
        }
    }

    bool CanTakeKey()
    {
        bool findBookMissionCompleted = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
        int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);
        return findBookMissionCompleted && documentCount >= 3;
    }

    public void GetKey()
    {
        if (!CanTakeKey())
        {
            Debug.Log("Tidak bisa mengambil kunci saat misi belum selesai.");
            alertKey.SetActive(true);
            Invoke("DisableAlertKeyMission", 2f);
            return;
        }

        takeKeyAudioSource.clip = takeKeyAudioClip;
        takeKeyAudioSource.Play();
        Debug.Log("Player mengambil kunci");
        keyObject.gameObject.SetActive(false);
        rewardInfo.SetActive(true);
        takeButton.gameObject.SetActive(false);
        PlayerPrefs.SetInt(keyMissionCompletedKey, 1);
        PlayerPrefs.Save();

        FindObjectOfType<ChestController>().ObtainKey();
    }

    private void DisableAlertKeyMission()
    {
        alertKey.SetActive(false);
    }

    public void ResetKeyMission()
    {
        Debug.Log("ResetKey");
        PlayerPrefs.SetInt(keyMissionCompletedKey, 0); 
        PlayerPrefs.Save();
        keyObject.SetActive(true); 
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
