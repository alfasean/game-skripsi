using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStartPosition : MonoBehaviour
{
    void Start()
    {
        // Ambil posisi terakhir pemain dari PlayerPrefs
        float playerPosX = PlayerPrefs.GetFloat("PlayerPosX", 0);
        float playerPosY = PlayerPrefs.GetFloat("PlayerPosY", 0);
        float playerPosZ = PlayerPrefs.GetFloat("PlayerPosZ", 0);

        // Posisikan pemain di posisi terakhirnya
        transform.position = new Vector3(playerPosX, playerPosY, playerPosZ);
    }
}
