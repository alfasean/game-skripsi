using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public GameObject bookImage; 
    private string findBookMissionKey = "FindBookMissionCompleted";

    public GameObject mapsImage;
    private string documentMissionCompletedKey = "DocumentMissionCompleted"; 

    public GameObject swordImage;
    private string chestOpenedKey = "swordandChest";

    public GameObject moldImage;
    private string enemyWarriorMissionCompleted = "EndDialogFarmer";

    public GameObject skillFireImage;
    public GameObject skillIceImage;
    private string owlMissionCompletedKey = "OwlMissionCompleted"; 

    public GameObject keyImage;
    private string wariorTakeKey = "wariorTakeKey";

    void Start()
    {
        bookImage.SetActive(false);
        bool missionCompletedBook = PlayerPrefs.GetInt(findBookMissionKey, 0) == 1;
        if (missionCompletedBook)
        {
            bookImage.SetActive(true);
        } else 
        {
            bookImage.SetActive(false);
        }

        mapsImage.SetActive(false);
        bool missionCompletedMaps = PlayerPrefs.GetInt(documentMissionCompletedKey, 0) == 1;
        if (missionCompletedMaps)
        {
            mapsImage.SetActive(true);
        } else 
        {
            mapsImage.SetActive(false);
        
        }

        keyImage.SetActive(false);
        bool missionCompletedkey = PlayerPrefs.GetInt(wariorTakeKey, 0) == 1;
        if (missionCompletedkey)
        {
            keyImage.SetActive(true);
        } else 
        {
            keyImage.SetActive(false);
        }

        UpdateInventory();

        moldImage.SetActive(false);
        bool missionCompletedFarmer = PlayerPrefs.GetInt(enemyWarriorMissionCompleted, 0) == 1;
        if (missionCompletedFarmer)
        {
            moldImage.SetActive(true);
        } else 
        {
            moldImage.SetActive(false);
        }

        skillFireImage.SetActive(false);
        skillIceImage.SetActive(false);
        bool missionCompletedWitch = PlayerPrefs.GetInt(owlMissionCompletedKey, 0) == 1;
        if (missionCompletedWitch)
        {
            skillFireImage.SetActive(true);
            skillIceImage.SetActive(true);
        } else 
        {
            skillFireImage.SetActive(false);
            skillIceImage.SetActive(false);
        }
    }

     public void UpdateInventory()
    {
        bool missionCompletedSword = PlayerPrefs.GetInt(chestOpenedKey, 0) == 1;
        swordImage.SetActive(missionCompletedSword);
    }
}
