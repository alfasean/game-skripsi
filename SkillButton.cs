using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    public Button skillButton; // Referensi ke tombol skill
    public Text cooldownText; // Referensi ke UI Text untuk menampilkan waktu cooldown

    private float cooldownDuration; // Durasi cooldown dalam detik
    private float cooldownTimer; // Timer untuk menghitung waktu cooldown yang ters
    private bool isCooldown; // Status apakah cooldown sedang berlangsung

    // Method untuk mengatur cooldown skill
    public void StartCooldown(float duration)
    {
        // Set durasi cooldown dan timer
        cooldownDuration = duration;
        cooldownTimer = cooldownDuration;
        isCooldown = true;

        // Matikan tombol skill
        skillButton.interactable = false;
    }

    void Update()
    {
        if (isCooldown)
        {
            // Kurangi waktu cooldown yang tersisa
            cooldownTimer -= Time.deltaTime;

            // Perbarui UI Text dengan waktu cooldown yang tersisa
            cooldownText.text = Mathf.Ceil(cooldownTimer).ToString();

            // Jika waktu cooldown telah habis
            if (cooldownTimer <= 0)
            {
                // Aktifkan kembali tombol skil
                skillButton.interactable = true;
                // Atur status cooldown menjadi false
                isCooldown = false;
                // Reset UI Text
                cooldownText.text = "";
            }
        }
    }
}