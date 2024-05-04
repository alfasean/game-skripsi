using UnityEngine;
using UnityEngine.UI;

public class OwlController : MonoBehaviour
{
    public Transform[] waypoints; 
    public float speed = 1f; 
    public float stoppingDistance = 0.1f; 
    private int currentWaypointIndex = 0;
    public Animator animator; 
    public Button takeButton; 
    public GameObject objectOwl; 
    private bool playerIsClose; 
    public AudioSource takeOwlAudioSource; 
    public AudioClip takeOwlAudioClip; 
    public bool owlTaken = false;

    void Start()
    {
        animator.SetBool("Flying", true);
        takeButton.onClick.AddListener(OnTakeButtonClick);
        
        if (PlayerPrefs.GetInt("OwlTaken", 0) == 1)
        {
            objectOwl.gameObject.SetActive(false); 
            owlTaken = true; 
        }
    }

    void Update()
    {
        
        transform.position = Vector3.Lerp(transform.position, waypoints[currentWaypointIndex].position, speed * Time.deltaTime);
        
        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        
        float movementSpeed = speed / 2; 
        animator.SetFloat("Speed", movementSpeed);

        
        takeButton.gameObject.SetActive(playerIsClose && !owlTaken);
    }

    public void OnTakeButtonClick()
    {
        
        takeOwlAudioSource.clip = takeOwlAudioClip;
        takeOwlAudioSource.Play();

        
        objectOwl.gameObject.SetActive(false);
        takeButton.gameObject.SetActive(false);

        
        owlTaken = true;
        
        PlayerPrefs.SetInt("OwlTaken", 1);
        PlayerPrefs.Save();
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
        }
    }

    
    public void GiveOwlToWitch()
    {
        owlTaken = true; 
    }
}
