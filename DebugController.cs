using UnityEngine;
using UnityEngine.UI;

public class DebugController : MonoBehaviour
{
    void Start()
    {
    }

#if UNITY_EDITOR
    public void ResetPlayerData()
    {
        PlayerPrefs.DeleteKey("PlayerName");
        Debug.Log("PlayerName reset.");
    }
#endif
}
