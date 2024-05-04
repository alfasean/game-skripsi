using UnityEngine;
using UnityEngine.UI;

public class BookButtonController : MonoBehaviour
{
    public Button bookButton;
    public GameObject bookMissionManager;

    void Start()
    {
        bookMissionManager.SetActive(false);
        
        bookButton.onClick.AddListener(OnBookButtonClick);
        
        UpdateBookButtonInteractability();
    }

    void Update()
    {    
        UpdateBookButtonInteractability();
    }

    void UpdateBookButtonInteractability()
    {
        
        bool bookMissionCompleted = PlayerPrefs.GetInt("BookMissionCompleted", 0) == 1;
        bookButton.interactable = bookMissionCompleted;
    }

    void OnBookButtonClick()
    {
        
        bookMissionManager.SetActive(true);
    }
    public void CloseBookButtonClick()
    {
        
        bookMissionManager.SetActive(false);
    }
}
