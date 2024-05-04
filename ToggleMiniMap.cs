using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ToggleMiniMap : MonoBehaviour
{
    public GameObject miniMap; // Objek mini map yang akan ditampilkan atau disembunyikan
    public Button mapButton; // Tombol untuk menampilkan atau menyembunyikan mini map

    void Start() 
    {
        // Cek apakah scene saat ini adalah "Stage1" atau "Stage2"
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Stage1" || sceneName == "Stage2")
        {
            // Jika scene adalah "Stage1" atau "Stage2", aktifkan mini map
            miniMap.SetActive(false);

            // Aktifkan tombol
            mapButton.interactable = true;
        }
        else
        {
            // Jika bukan "Stage1" atau "Stage2", nonaktifkan mini map
            // miniMap.SetActive(false);

            // Nonaktifkan tombol
            mapButton.interactable = false;
        }
    }

    public void ToggleMapVisibility()
    {
        // Ubah status aktif/nonaktif objek mini map saat tombol ditekan
        miniMap.SetActive(!miniMap.activeSelf);
    }
}
