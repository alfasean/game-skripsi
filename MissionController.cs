using UnityEngine;
using UnityEngine.UI;

public class MissionController : MonoBehaviour
{
    public GameObject[] missions;
    public Button prevButton;
    public Button nextButton;
    public Button prevMentokButton;
    public Button nextMentokButton;
    // Stage 1
    private string chestOpenedKey = "swordandChest";
    public GameObject completeMissionSword;
    private bool isChestOpened = false;

    private string findBookMissionKey = "FindBookMissionCompleted";
    public GameObject completeMissionBook;
    private bool isFindBook = false;

    // private string documentMissionCompletedKey = "DocumentMissionCompleted"; 
    // public GameObject completeMissionMaps;
    // private bool isFindMaps = false;

     private string documentMissionKey = "DocumentCount";
    //  public GameObject[] completeMissionObjects; 


    // Stage 2
    private string owlMissionCompletedKey = "OwlMissionCompleted"; 
    public GameObject completeMissionOwl;
    private bool isOwlMissionComplete = false;

    private string enemyWarriorMissionCompleted = "EnemyWarriorMissionCompleted";
    public GameObject completeMissionFightPrajurit;
    private bool isFightPrajuritMissionComplete = false;

    private string enemyCaptainMissionCompleted = "EnemyCaptainMissionCompleted";
    public GameObject completeMissionFightKapten;
    private bool isFightKaptenMissionComplete = false;

    private int currentMissionIndex = 0;

    void Start()
    {
        //  Stage1 1
        isChestOpened = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
            if (isChestOpened)
            {
                completeMissionSword.SetActive(true);
            }
            else
        {
            completeMissionSword.SetActive(false);
        }

        isFindBook = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
            if (isFindBook)
            {
                completeMissionBook.SetActive(true);
            }
            else
        {
            completeMissionBook.SetActive(false);
        }

        // isFindMaps = PlayerPrefs.GetInt(documentMissionCompletedKey, 0) == 1;
        //     if (isFindMaps)
        //     {
        //         completeMissionMaps.SetActive(true);
        //     }
        //     else
        // {
        //     completeMissionMaps.SetActive(false);
        // }

        // int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);
        // documentCount++;
        // PlayerPrefs.SetInt(documentMissionKey, documentCount);
        // UpdateDocumentMissionText();

        
        // if (documentCount < 3)
        // {
        //     foreach (GameObject completeMission in completeMissionObjects)
        //     {
        //         completeMission.SetActive(false);
        //     }
        // }


        // Stage 2

        isOwlMissionComplete = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;
            if (isOwlMissionComplete)
            {
                completeMissionOwl.SetActive(true);
            }
            else
        {
            completeMissionOwl.SetActive(false);
        }

        isFightPrajuritMissionComplete = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
            if (isFightPrajuritMissionComplete)
            {
                completeMissionFightPrajurit.SetActive(true);
            }
            else
        {
            completeMissionFightPrajurit.SetActive(false);
        }

        isFightKaptenMissionComplete = PlayerPrefs.GetInt(enemyCaptainMissionCompleted, 0) == 1;
            if (isFightKaptenMissionComplete)
            {
                completeMissionFightKapten.SetActive(true);
            }
            else
        {
            completeMissionFightKapten.SetActive(false);
        }


        ShowMission(currentMissionIndex);
    }

    // private void UpdateDocumentMissionText()
    // {
    //     int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);
    //     // for (int i = 0; i < documentMissionTexts.Length; i++)
    //     // {
    //     //     documentMissionTexts[i].text = "(" + documentCount + "/3)";
    //     // }

        
    //     if (documentCount >= 3)
    //     {
    //         foreach (GameObject completeMission in completeMissionObjects)
    //         {
    //             completeMission.SetActive(true);
    //         }
    //     }
    //     else
    //     {
    //         foreach (GameObject completeMission in completeMissionObjects)
    //         {
    //             completeMission.SetActive(false);
    //         }
    //     }
    // }

    public void NextMission()
    {
        if (currentMissionIndex < missions.Length - 1)
        {
            currentMissionIndex++;
            UpdateButtons();
            ShowMission(currentMissionIndex);
        }
    }

    public void PreviousMission()
    {
        if (currentMissionIndex > 0)
        {
            currentMissionIndex--;
            UpdateButtons();
            ShowMission(currentMissionIndex);
        }
    }


    void ShowMission(int index)
    {
        for (int i = 0; i < missions.Length; i++)
        {
            missions[i].SetActive(i == index);
        }
    }

    void UpdateButtons()
    {
        prevButton.gameObject.SetActive(currentMissionIndex > 0);
        nextButton.gameObject.SetActive(currentMissionIndex < missions.Length - 1);
        prevMentokButton.gameObject.SetActive(currentMissionIndex == 0);
        nextMentokButton.gameObject.SetActive(currentMissionIndex == missions.Length - 1);
    }
}
