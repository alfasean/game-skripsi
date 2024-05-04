using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    private Animator animator;
    private FightSceneController fightSceneController;

    private AudioSource audioSource;
    public AudioClip basicAttackSound;
    public AudioClip skill1Sound;
    public GameObject thunderAnimation; 
    private bool isFightStarted = false;

    public float enemyBasicAttackDamage = 10f;
    public float enemySkill1Damage = 20f;

    public float maxHealth = 200f;
    public float currentHealth;

    public Slider healthBar;

    [SerializeField] Transform target;
    NavMeshAgent agent;

    public float movementSpeed = 2f;

    public float skillRange = 0.9f;
    public float skillCooldown = 0f;

    private float nextSkillTime = 0f;
    private Vector3 initialEnemyPosition;

    private bool isUsingSkill1 = true;
    private WaitForSeconds thunderDuration = new WaitForSeconds(1f); 
    private WaitForSeconds skill1CooldownDuration = new WaitForSeconds(8f);

    private bool skill1OnCooldown = false; 

    void Start()
    {
        initialEnemyPosition = transform.position;
        animator = GetComponent<Animator>();
        fightSceneController = FindObjectOfType<FightSceneController>();
        audioSource = GetComponent<AudioSource>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        thunderAnimation.SetActive(false);

        agent.speed = movementSpeed;

        currentHealth = maxHealth;

        healthBar.maxValue = maxHealth;

        healthBar.value = currentHealth;
    }

    private void Update()
    {
        if (!isFightStarted)
            return;
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget > skillRange)
        {
            agent.SetDestination(target.position);

            bool isMoving = agent.velocity.magnitude > 0.1f;

            animator.SetBool("isMoving", isMoving);
        }
        else
        {
            agent.velocity = Vector3.zero;

            animator.SetBool("isMoving", false);
        }

        if (distanceToTarget <= skillRange && Time.time >= nextSkillTime)
        {
            UseSkill();
        }

        target.position = GameObject.FindGameObjectWithTag("Player").transform.position;
    }

    void UseSkill()
    {
                if (!isFightStarted)
            return;
        if (currentHealth <= 0) return; 
        if (isUsingSkill1 && !skill1OnCooldown)
        {
            EnemySkill1();
        }
        else
        {
            EnemyBasicAttack();
        }

        isUsingSkill1 = !isUsingSkill1;

        nextSkillTime = Time.time + skillCooldown;
    }

    void EnemySkill1()
    {

        animator.SetTrigger("skill1");
        audioSource.PlayOneShot(skill1Sound);
        PlayerCombat playerCombat = target.GetComponent<PlayerCombat>();
        if (playerCombat != null)
        {
            playerCombat.PlayerTakeDamage(enemySkill1Damage);
        }
        if (thunderAnimation != null)
        {
            thunderAnimation.SetActive(true);
            StartCoroutine(DisableThunderAnimation());
        }

        StartCoroutine(StartSkill1Cooldown());
    }

    IEnumerator DisableThunderAnimation()
    {
        yield return thunderDuration; 
        thunderAnimation.SetActive(false); 
    }

    IEnumerator StartSkill1Cooldown()
    {
        skill1OnCooldown = true; 
        yield return skill1CooldownDuration; 
        skill1OnCooldown = false; 
    }

    void EnemyBasicAttack()
    {
        animator.SetTrigger("basicAttack");
        audioSource.PlayOneShot(basicAttackSound);

        PlayerCombat playerCombat = target.GetComponent<PlayerCombat>();
        if (playerCombat != null)
        {
            playerCombat.PlayerTakeDamage(enemyBasicAttackDamage);
        }
    }

    public void StartFight()
    {
        isFightStarted = true;
    }

    public void EnemyTakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.value = currentHealth;

        if (currentHealth <= 0)
        {
            EnemyDie();
        }
    }

    void EnemyDie()
    {
        Debug.Log("Enemy Lose");
        fightSceneController.PlayerScores();
    }

    public void TriggerWinAnimation()
    {
        animator.SetTrigger("victory");
    }

    public void TriggerDieAnimation()
    {
        animator.SetTrigger("die");
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

    public void ResetEnemyPosition()
    {
        transform.position = initialEnemyPosition;
    }
}
