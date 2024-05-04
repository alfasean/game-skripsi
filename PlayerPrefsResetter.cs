using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefsResetter : MonoBehaviour
{
    public Button resetButton;

    void Start()
    {
        resetButton.onClick.AddListener(ResetPlayerPrefs);
    }

    private void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("PlayerPrefs telah direset.");
    }
}
