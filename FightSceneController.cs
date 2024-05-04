using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FightSceneController : MonoBehaviour
{
    public GameObject winPanel; 
    public GameObject losePanel; 

    public Image roundOneImage; 
    public Image roundTwoImage; 
    public Image roundThreeImage; 
    public PlayerCombat player; 
    public EnemyCombat enemy; 
    public Image[] playerRoundScores; 
    public Image[] enemyRoundScores; 

    public AudioClip fightSoundtrack; 
    private AudioSource audioSource;

    private int playerScore = 0; 
    private int enemyScore = 0; 
    private int currentRound = 1; 

    public Image[] countdownImages; 

    // Start scene fight
    void Start()
    {
        winPanel.SetActive(false);
        losePanel.SetActive(false);
        StartCoroutine(ShowRoundAndCountdown());

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = fightSoundtrack;
        audioSource.loop = true;
        audioSource.Play();
    }

    void OnDestroy()
    {
        if (audioSource != null)
        {
            audioSource.Stop();
            Destroy(audioSource);
        }
    }


    // Countdown and Round 1
    IEnumerator ShowRoundAndCountdown()
    {
        
        StartCoroutine(ShowRoundImage(roundOneImage));

        
        yield return new WaitForSeconds(1f);

        
        for (int i = 0; i < countdownImages.Length; i++)
        {
            countdownImages[i].gameObject.SetActive(true); 
            yield return new WaitForSeconds(1f);
            countdownImages[i].gameObject.SetActive(false); 
        }

        StartFight();
    }

    // Pemain vs Musuh
    void StartFight()
    {
        player.StartFight();
        enemy.StartFight();
    }

    // Round 2 and round 3
    IEnumerator ShowRoundImage(Image roundImage)
    {
        roundImage.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        roundImage.gameObject.SetActive(false);
    }

    public void PlayerScores()
    {
        playerScore++;
        UpdateRoundScores();
        CheckRoundEnd();
    }

    public void EnemyScores()
    {
        enemyScore++;
        UpdateRoundScores();
        CheckRoundEnd();
    }

    void UpdateRoundScores()
    {
        roundOneImage.gameObject.SetActive(false);
        roundTwoImage.gameObject.SetActive(false);
        roundThreeImage.gameObject.SetActive(false);

        foreach (Image playerRoundScore in playerRoundScores)
        {
            playerRoundScore.gameObject.SetActive(false);
        }

        foreach (Image enemyRoundScore in enemyRoundScores)
        {
            enemyRoundScore.gameObject.SetActive(false);
        }

        for (int i = 0; i < playerScore; i++)
        {
            playerRoundScores[i].gameObject.SetActive(true);
        }

        for (int i = 0; i < enemyScore; i++)
        {
            enemyRoundScores[i].gameObject.SetActive(true);
        }

        switch (currentRound)
        {
            case 1:
                StartCoroutine(ShowRoundImage(roundTwoImage));
                break;
            case 2:
                StartCoroutine(ShowRoundImage(roundThreeImage));
                break;
        }
    }

    // Penilaian Hasil
    void CheckRoundEnd()
    {
        if (playerScore >= 2 || enemyScore >= 2 || currentRound >= 3)
        {
            string winner = "";
            if (playerScore > enemyScore) {
                winner = "Player";
                enemy.TriggerDieAnimation();
                player.TriggerWinAnimation();
                player.StopSound();
                enemy.StopSound();
                roundThreeImage.gameObject.SetActive(false);
                winPanel.SetActive(true); 
            } else if (playerScore < enemyScore) {
                winner = "Enemy";
                player.TriggerDieAnimation();
                enemy.TriggerWinAnimation();
                player.StopSound();
                enemy.StopSound();
                roundThreeImage.gameObject.SetActive(false);
                losePanel.SetActive(true); 
            } else {
                Debug.Log("Game Over! It's a draw!");
                return;
            }

            Debug.Log("Game Over! " + winner + " wins!");
        }
        else
        {
            currentRound++;
            RestartRound();
        }
    }

    void RestartRound()
    {
        player.ResetHealth(); 
        enemy.ResetHealth(); 
        player.ResetPlayerPosition(); 
        enemy.ResetEnemyPosition(); 
        player.StopSound();
        enemy.StopSound();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    public void ExitGame()
    {
        SceneManager.LoadScene("Stage2"); 
    }

    public void ExitGameTamat()
    {
        SceneManager.LoadScene("LoadingTamat"); 
    }
}
