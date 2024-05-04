using UnityEngine;

public class PlayerPositionInput : MonoBehaviour
{
    public Transform playerStartPosition;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SavePlayerStartPosition();
            // SceneManager.LoadScene("Stage2");
        }
    }

    private void SavePlayerStartPosition()
    {
        if (playerStartPosition != null)
        {
            PlayerPrefs.SetFloat("PlayerStartPosX", playerStartPosition.position.x);
            PlayerPrefs.SetFloat("PlayerStartPosY", playerStartPosition.position.y);
            PlayerPrefs.SetFloat("PlayerStartPosZ", playerStartPosition.position.z);
        }
    }

    public void ClearPlayerStartPosition()
    {
        PlayerPrefs.DeleteKey("PlayerStartPosX");
        PlayerPrefs.DeleteKey("PlayerStartPosY");
        PlayerPrefs.DeleteKey("PlayerStartPosZ");
        Debug.Log("Reset Position Success");
    }
}
