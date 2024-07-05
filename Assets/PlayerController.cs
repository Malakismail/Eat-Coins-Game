using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    public TextMeshProUGUI countText; // Reference to the UI Text component
    public GameObject winTextObject;

    private int count; // Counter for cubes eaten by the player

    public AudioManager audioManager;

    void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    void Start()
    {
        count = 0;
        UpdateCountText(); // Update the count text initially

        // Set the text property of the Win Text UI to an empty string, making the 'You Win' (game over message) blank
        winTextObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }

    // Handle collision between player and PickUp objects
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Cubes"))
        {
            Destroy(other.gameObject);
            count = count + 1; // Increment count when player eats a cube
            audioManager.PlaySFX(audioManager.eatCoins);
            UpdateCountText(); // Update the count text
        }
        if (other.gameObject.CompareTag("increase_10"))
        {
            Destroy(other.gameObject);
            count = count + 10; // Increment count when player eats a cube
            audioManager.PlaySFX(audioManager.eatCoins);
            UpdateCountText(); // Update the count text
        }
    }

    // Update the count text displayed on the screen
    void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 23)
        {

            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
            audioManager.PlaySFX(audioManager.levelUp);
        }
    }

}