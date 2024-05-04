using UnityEngine;
using UnityEngine.UI;

public class MissionManager_InteractPlant : MonoBehaviour
{
    public Text plantMissionText;
    private string plantMissionKey = "PlantCount"; 
    public GameObject CompleteMission; 
    private string plantMissionCompletedKey = "PlantMissionCompleted"; 

    void Start()
    {
        LoadPlantMissionProgress(); 
        UpdatePlantMissionText();
        // CompleteMission.SetActive(false);
    }

    public void InteractWithPlant()
    {
        PlayerPrefs.SetInt(plantMissionKey, 1);
        PlayerPrefs.DeleteKey(plantMissionCompletedKey); 
        UpdatePlantMissionText();
    }

    private void UpdatePlantMissionText()
    {
        int plantCount = PlayerPrefs.GetInt(plantMissionKey, 0);
        if (plantCount >= 1)
        {
            CompleteMission.SetActive(true);
        }
        else
        {
            CompleteMission.SetActive(false);
        }
    }

    private void SavePlantMissionProgress()
    {
        PlayerPrefs.Save();
    }

    private void LoadPlantMissionProgress()
    {
        
    }

    public void ResetPlantMission()
    {
        PlayerPrefs.DeleteKey(plantMissionKey); 
        SavePlantMissionProgress();
        PlayerPrefs.DeleteKey(plantMissionCompletedKey); 
        UpdatePlantMissionText();
        Debug.Log("Mission Reset");
    }
}