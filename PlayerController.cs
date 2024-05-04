using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    public LayerMask solidObjectsLayer;
    public VariableJoystick joystick;
    private string currentScene;

    public AudioSource footstepAudioSource;
    public AudioClip footstepSound;

    public static PlayerController Instance;
    private bool canMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        currentScene = SceneManager.GetActiveScene().name;

        if (currentScene == "Market" || currentScene == "KadesHome" || currentScene == "ResidentHome" || currentScene == "FoodStorage" || currentScene == "GoaCompleteMission")
        {
            animator.SetFloat("moveY", 1);
        }
        if (currentScene == "Stage2" && PreviousSceneName() == "Market")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "KadesHome")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "ResidentHome")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "FoodStorage")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "FoodStorage")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1MissionComplete" && PreviousSceneName() == "GoaCompleteMission")
        {
            SetPlayerStartPosition();
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "Stage2")
        {
            SetPlayerStartPosition();
            animator.SetFloat("moveY", 1);
        }
        if (currentScene == "Goa" && PreviousSceneName() == "Stage1")
        {
            SetPlayerStartPosition();
            animator.SetFloat("moveY", 1);
        }
        if (currentScene == "Stage1MissionComplete" && PreviousSceneName() == "Stage2")
        {
            SetPlayerStartPosition();
            animator.SetFloat("moveY", 1);
        }
        if (currentScene == "Stage1" && PreviousSceneName() == "TalkKades")
        {
            SetPlayerStartPosition();
            animator.SetFloat("moveY", 1);
        }
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        input.x = joystick.Horizontal;
        input.y = joystick.Vertical;

        if (input == Vector2.zero)
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);

            footstepAudioSource.Stop();
            return;
        }

        animator.SetFloat("moveX", input.x);
        animator.SetFloat("moveY", input.y);

        var targetPos = transform.position + new Vector3(input.x, input.y, 0);

        if (isWalkable(targetPos))
        {
            Move(targetPos);
            if (!footstepAudioSource.isPlaying)
            {
                footstepAudioSource.clip = footstepSound;
                footstepAudioSource.Play();
            }
        }
    }

    public void SetCanMove(bool value)
    {
        canMove = value;
    }

    public bool CanMove()
    {
        return canMove;
    }

    private void Move(Vector3 targetPos)
    {
        isMoving = true;
        animator.SetBool("isMoving", isMoving);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0, solidObjectsLayer) != null)
        {
            return false;
        }

        return true;
    }

    private string PreviousSceneName()
    {
        return PlayerPrefs.GetString("PreviousScene", "");
    }

    private void SetPlayerStartPosition()
    {
        float startX = PlayerPrefs.GetFloat("PlayerStartPosX", 0);
        float startY = PlayerPrefs.GetFloat("PlayerStartPosY", 0);
        float startZ = PlayerPrefs.GetFloat("PlayerStartPosZ", 0);

        transform.position = new Vector3(startX, startY, startZ);
    }
}
