using UnityEngine;
using UnityEngine.UI;

public class MissionManager_FindHistoricalDocument : MonoBehaviour
{
    public Text[] documentMissionTexts; 
    private string documentMissionKey = "DocumentCount";
    private string documentMissionCompletedKey = "DocumentMissionCompleted"; 
    public GameObject[] completeMissionObjects; 

    void Start()
    {
        LoadDocumentMissionProgress(); 
        UpdateDocumentMissionText();
        ResetCompleteMissionState(); 
    }

    public void FoundDocument()
    {
        int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);
        documentCount++;
        PlayerPrefs.SetInt(documentMissionKey, documentCount);
        UpdateDocumentMissionText();

        
        if (documentCount < 3)
        {
            foreach (GameObject completeMission in completeMissionObjects)
            {
                completeMission.SetActive(false);
            }
        }
    }

    private void UpdateDocumentMissionText()
    {
        int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);
        for (int i = 0; i < documentMissionTexts.Length; i++)
        {
            documentMissionTexts[i].text = "(" + documentCount + "/3)";
        }

        
        if (documentCount >= 3)
        {
            foreach (GameObject completeMission in completeMissionObjects)
            {
                completeMission.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject completeMission in completeMissionObjects)
            {
                completeMission.SetActive(false);
            }
        }
    }

    private void SaveDocumentMissionProgress()
    {
        PlayerPrefs.Save();
    }

    private void LoadDocumentMissionProgress()
    {
        
        int documentCount = PlayerPrefs.GetInt(documentMissionKey, 0);

        
        if (documentCount < 3)
        {
            foreach (GameObject completeMission in completeMissionObjects)
            {
                completeMission.SetActive(false);
            }
        }
    }

    private void ResetCompleteMissionState()
    {
        
        foreach (GameObject completeMission in completeMissionObjects)
        {
            completeMission.SetActive(false);
        }
    }

    public void ResetDocumentMission()
    {
        PlayerPrefs.DeleteKey(documentMissionKey); 
        PlayerPrefs.DeleteKey(documentMissionCompletedKey); 
        SaveDocumentMissionProgress();
        UpdateDocumentMissionText();
        ResetCompleteMissionState(); 
        Debug.Log("Mission Reset");
    }
}
