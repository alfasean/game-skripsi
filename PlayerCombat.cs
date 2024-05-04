using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class PlayerCombat : MonoBehaviour
{
    private Animator animator;
    private GameObject nearestEnemy;
    private bool isPlayerInRange;
    private FightSceneController fightSceneController;
    private AudioSource audioSource;
    public AudioClip basicAttackSound;
    public AudioClip skill1Sound;
    public AudioClip skill2Sound;
    public AudioClip addHealthSound;
    private bool isFightStarted = false;

    public float attackRange = 1.5f;
    public float playerBasicAttackDamage = 3f;
    public float playerSkill1Damage = 20f;
    public float playerSkill2Damage = 30f;

    public float maxHealth = 100f;
    public float currentHealth;
    private bool canAddHealth = true;
    public Button healthButton;
    private int healthAddCount = 0;

    public Slider healthBar;

    public GameObject iceAnimation; 

    private float skill1Cooldown = 3f;
    private float skill2Cooldown = 5f;
    private float nextSkill1Time = 0f;
    private float nextSkill2Time = 0f;
    private Vector3 initialPlayerPosition;
    private WaitForSeconds iceDuration = new WaitForSeconds(1f); 

    void Start()
    {
        initialPlayerPosition = transform.position;
        animator = GetComponent<Animator>();
        fightSceneController = FindObjectOfType<FightSceneController>();
        audioSource = GetComponent<AudioSource>();

        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;

        healthBar.value = currentHealth;
        iceAnimation.SetActive(false);
    }

    void Update()
    {
        if (!isFightStarted)
            return;
        if (nearestEnemy != null)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, nearestEnemy.transform.position);
            isPlayerInRange = distanceToEnemy <= attackRange;
        }
    }

    public void StartFight()
    {
        isFightStarted = true;
    }

    public void PlayerActivateSkill1()
    {
        if (!isFightStarted)
            return; 
        if (Time.time >= nextSkill1Time)
        {
            animator.SetTrigger("skill1");
            audioSource.PlayOneShot(skill1Sound);

            if (isPlayerInRange)
            {
                AttackEnemy(nearestEnemy, playerSkill1Damage);
            }

            nextSkill1Time = Time.time + skill1Cooldown;
        }
    }

    public void PlayerActivateSkill2()
    {
        if (!isFightStarted)
            return; 
        if (Time.time >= nextSkill2Time)
        {
            animator.SetTrigger("skill2");
            audioSource.PlayOneShot(skill2Sound);

            if (isPlayerInRange)
            {
                AttackEnemy(nearestEnemy, playerSkill2Damage);
            }

            nextSkill2Time = Time.time + skill2Cooldown;

            if (iceAnimation != null)
            {
                iceAnimation.SetActive(true);
                StartCoroutine(DisableIceAnimation());
            }
        }
    }

    IEnumerator DisableIceAnimation()
    {
        yield return iceDuration; 
        iceAnimation.SetActive(false); 
    }

    public void PlayerBasicAttack()
    {
        if (!isFightStarted)
            return; 
        animator.SetTrigger("basicAttack");
        audioSource.PlayOneShot(basicAttackSound);

        if (isPlayerInRange)
        {
            AttackEnemy(nearestEnemy, playerBasicAttackDamage);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearestEnemy = other.gameObject;
            Debug.Log("Player entered enemy's trigger zone");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == nearestEnemy)
        {
            nearestEnemy = null;
            Debug.Log("Player exited enemy's trigger zone");
        }
    }

    void AttackEnemy(GameObject enemy, float damage)
    {
        if (enemy != null)
        {
            EnemyCombat enemyCombat = enemy.GetComponent<EnemyCombat>();
            if (enemyCombat != null)
            {
                enemyCombat.EnemyTakeDamage(damage);
            }
        }
    }

    public void PlayerTakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            PlayerDie();
        }
    }

    public void AddHealth()
    {
        if (canAddHealth && currentHealth < maxHealth && healthAddCount < 2) 
        {
            animator.SetTrigger("addHealth");
            audioSource.PlayOneShot(addHealthSound);
            currentHealth += 30f;
            if (currentHealth > maxHealth)
                currentHealth = maxHealth;

            healthBar.value = currentHealth;
            healthAddCount++; 
            if (healthAddCount >= 2) 
                healthButton.interactable = false;
        }
    }

    void PlayerDie()
    {
        Debug.Log("Player Lose");
        fightSceneController.EnemyScores();
    }

    public void TriggerDieAnimation()
    {
        animator.SetTrigger("die");
    }

    public void TriggerWinAnimation()
    {
        animator.SetTrigger("winAnimation");
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    }

    public void StopSound()
    {
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    public void ResetPlayerPosition()
    {
        transform.position = initialPlayerPosition;
    }
}
