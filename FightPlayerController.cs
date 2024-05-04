using UnityEngine;

public class FightPlayerController : MonoBehaviour
{
    public float moveSpeed;
    private bool isMoving;
    private Vector2 input;

    private Animator animator;
    public LayerMask solidObjectsLayer;
    public VariableJoystick joystick;

    public static FightPlayerController Instance;
    private bool canMove = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!canMove)
        {
            return;
        }
        input.x = joystick.Horizontal;
        // input.y = joystick.Vertical;

        if (input == Vector2.zero)
        {
            isMoving = false;
            animator.SetBool("isMoving", isMoving);
            return;
        }

        animator.SetFloat("moveX", input.x);
        // animator.SetFloat("moveY", input.y);

        var targetPos = transform.position + new Vector3(input.x, 0);

        if (isWalkable(targetPos))
        {
            Move(targetPos);
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
}
