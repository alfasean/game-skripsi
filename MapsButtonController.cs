using UnityEngine;
using UnityEngine.UI;

public class MapsButtonController : MonoBehaviour
{
    public Button mapsButton;
    public GameObject mapPanel;

    void Start()
    {
        mapPanel.SetActive(false);
        
        mapsButton.onClick.AddListener(OnmapsButtonClick);
        
        UpdatemapsButtonInteractability();
    }

    void Update()
    {    
        UpdatemapsButtonInteractability();
    }

    void UpdatemapsButtonInteractability()
    {
        
        bool mapsMissionCompleted = PlayerPrefs.GetInt("DocumentMissionCompleted", 0) == 1;
        mapsButton.interactable = mapsMissionCompleted;
    }

    void OnmapsButtonClick()
    {
        
        mapPanel.SetActive(true);
    }
    public void ClosemapsButtonClick()
    {
        
        mapPanel.SetActive(false);
    }
}
