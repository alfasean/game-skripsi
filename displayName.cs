using UnityEngine;
using UnityEngine.UI;

public class displayName : MonoBehaviour
{
    public Text nameText; 

    private void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName", "");

        nameText.text =  playerName;
    }
}
