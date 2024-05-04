using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class BoatController : MonoBehaviour
{
    [SerializeField] Transform target;
    NavMeshAgent agent;
    private Animator animator;

    public Button interactButton;
    public float interactDistance = 3f;
    public GameObject playerGameObject;
    private bool playerIsClose;
    private bool isInteracting;
    private bool isPlayerOnBoat;

    private Vector3 playerInitialPosition;
    private Vector3 boatInitialPosition;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        animator = GetComponent<Animator>();

        interactButton.onClick.AddListener(InteractWithPlayer);

        interactButton.gameObject.SetActive(false);
        isInteracting = false;
        isPlayerOnBoat = false;

        playerInitialPosition = new Vector3(-7.94f, 2.11f, 0f);
        boatInitialPosition = transform.position; // Set posisi awal perahu hanya sekali di awal
    }

    private void Update()
    {
        if (isPlayerOnBoat)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.1f)
            {
                MovePlayerToInitialPosition();
            }

            float speed = agent.velocity.magnitude;
            animator.SetFloat("Speed", speed);

            Vector3 dir = agent.velocity.normalized;
            float angle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg;
            animator.SetFloat("MoveX", Mathf.Sin(angle * Mathf.Deg2Rad));
            animator.SetFloat("MoveY", Mathf.Cos(angle * Mathf.Deg2Rad));

            // Pengecekan jarak antara pemain dan perahu
            float distanceToBoat = Vector3.Distance(playerGameObject.transform.position, transform.position);
            if (distanceToBoat < interactDistance)
            {
                interactButton.gameObject.SetActive(true);
            }
            else
            {
                interactButton.gameObject.SetActive(false);
            }
        }
        else if (playerIsClose && !isInteracting)
        {
            interactButton.gameObject.SetActive(true);
        }
    }

    public void InteractWithPlayer()
    {
        if (isPlayerOnBoat)
        {
            // Jika pemain sudah berada di perahu dan menekan tombol lagi, kembalikan ke posisi awal
            MovePlayerToInitialPosition();
        }
        else
        {
            Debug.Log("Player naik ke kapal");

            playerGameObject.SetActive(false);
            interactButton.gameObject.SetActive(false);
            isInteracting = true;

            // Memulai animasi boat bergerak ke target
            agent.SetDestination(target.position);

            // Memindahkan player setelah animasi berlangsung
            Invoke("MovePlayerToInitialPosition", 2f);
        }
    }

    private void MovePlayerToInitialPosition()
    {
        // Menonaktifkan animasi boat bergerak
        agent.ResetPath();

        playerGameObject.SetActive(true);

        // Memindahkan player ke posisi baru menggunakan Transform.Translate
        playerGameObject.transform.position = playerInitialPosition;

        // Nonaktifkan NavMeshAgent pada player
        NavMeshAgent playerNavMeshAgent = playerGameObject.GetComponent<NavMeshAgent>();
        if (playerNavMeshAgent != null)
        {
            playerNavMeshAgent.enabled = false;
        }

        // Mengaktifkan kembali kontrol pemain
        PlayerController playerController = playerGameObject.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.SetCanMove(true);
        }

        // Reset status interaksi
        isPlayerOnBoat = false;
        isInteracting = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerIsClose = false;
            // Tambahkan pengecekan jarak di sini juga
            float distanceToBoat = Vector3.Distance(playerGameObject.transform.position, transform.position);
            if (distanceToBoat < interactDistance)
            {
                interactButton.gameObject.SetActive(true);
            }
            else
            {
                interactButton.gameObject.SetActive(false);
            }
        }
    }
}
