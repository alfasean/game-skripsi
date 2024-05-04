using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChestController : MonoBehaviour
{
    public InventoryManager InventoryManager;
    public Button openButton;
    public GameObject popUpPanel;
    public GameObject rewardInfo;
    public GameObject alertKey;
    public Animator chestAnimator;
    public AudioSource chestAudioSource;
    public AudioClip chestOpenAudioClip;
    public GameObject completeMission; 
    private string chestOpenedKey = "swordandChest";
    private bool playerIsClose;
    private bool hasKey = false;
    private bool isChestOpened = false;
    private string sceneToLoad = "TalkKades";

    void Start()
    {
        chestAnimator = GetComponent<Animator>();
        popUpPanel.SetActive(false);
        rewardInfo.SetActive(false);
        alertKey.SetActive(false);
        openButton.gameObject.SetActive(false);

        if (PlayerPrefs.HasKey(chestOpenedKey))
        {
            isChestOpened = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
            Debug.Log("isChestOpened: " + isChestOpened); 
            if (isChestOpened)
            {
                Debug.Log("Peti sudah terbuka sebelumnya."); 
                completeMission.SetActive(true);
                chestAnimator.SetTrigger("OpenPosition");
                openButton.gameObject.SetActive(false);
            }
        }
        else
        {
            completeMission.SetActive(false);
            Debug.Log("Tidak ada kunci chestOpenedKey dalam PlayerPrefs."); 
        }
    }

    // void Update()
    // {
    //     if (playerIsClose && !isChestOpened)
    //     {
    //         openButton.gameObject.SetActive(true);
    //     }
    //     else
    //     {
    //         openButton.gameObject.SetActive(false);
    //     }
    // }

    public void OnTakeButtonClick() 
    {
        popUpPanel.SetActive(true);
        openButton.gameObject.SetActive(false);
    }

    public void OpenChest()
    {
        if (hasKey)
        {
            chestAnimator.SetTrigger("Open");
            rewardInfo.SetActive(true);
            popUpPanel.SetActive(false);

            PlayerPrefs.SetInt(chestOpenedKey, 1); 
            PlayerPrefs.Save(); 
            Debug.Log("Peti berhasil dibuka."); 

            chestAudioSource.clip = chestOpenAudioClip;
            chestAudioSource.Play();
            completeMission.SetActive(true);
            openButton.gameObject.SetActive(false);
            InventoryManager.UpdateInventory();
        }
        else
        {
            Debug.Log("Temukan kunci terlebih dahulu!");
            alertKey.SetActive(true);
            popUpPanel.SetActive(false);
            Invoke("DisableAlertKey", 2f);
        }
    }

    private void DisableAlertKey()
    {
        alertKey.SetActive(false);
    }

    public void ClosePopUp()
    {
        popUpPanel.SetActive(false);
    }

    public void ObtainKey()
    {
        hasKey = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isComplete = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (!isComplete) {
            playerIsClose = true;
            openButton.gameObject.SetActive(true); 
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        bool isComplete = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
        if (other.CompareTag("Player"))
        {
            if (!isComplete) {
            playerIsClose = false;
            openButton.gameObject.SetActive(false); 
            }
        }
    }
}
