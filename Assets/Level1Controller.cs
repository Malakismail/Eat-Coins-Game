using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;
using TMPro;

public class Level1Controller : MonoBehaviour
{
    public float speed = 3f; // Player movement speed

    public TextMeshProUGUI countText; // Reference to the UI Text component
    public GameObject winTextObject;
    public GameObject nextLevel;
    public GameObject lossTextObject;
    public GameObject lossLevel;

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
        nextLevel.SetActive(false);
        lossTextObject.SetActive(false);
        lossLevel.SetActive(false);
    }

    void Update()
    {
        // Handle player movement with keyboard input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * speed * Time.deltaTime;
        transform.Translate(movement);

        // Check for player collision with other objects
        CheckCollisions();
    }

    void CheckCollisions()
    {
        // Handle collision between player and PickUp objects
        foreach (Collider collider in Physics.OverlapSphere(transform.position, 0.5f))
        {
            if (collider.gameObject.CompareTag("Cubes"))
            {
                Destroy(collider.gameObject);
                count++; // Increment count when player eats a cube
                audioManager.PlaySFX(audioManager.eatCoins);
                UpdateCountText(); // Update the count text
            }
            else if (collider.gameObject.CompareTag("increase_10"))
            {
                Destroy(collider.gameObject);
                count += 10; // Increment count when player eats an increase_10 cube
                audioManager.PlaySFX(audioManager.eatCoins);
                UpdateCountText(); // Update the count text
            }
            else if (collider.gameObject.CompareTag("Walls"))
            {
                lossTextObject.SetActive(true);
                lossLevel.SetActive(true);
                // For example, you can restart the game after a delay
                // Invoke("RestartGame", 2f);
            }
        }
    }

    /*// Handle collision with walls
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            // Display loss text and perform other loss-related actions
            lossTextObject.SetActive(true);
            // For example, you can restart the game after a delay
            Invoke("RestartGame", 2f);
        }
    }*/

    // Update the count text displayed on the screen
    void UpdateCountText()
    {
        countText.text = "Count: " + count.ToString();

        if (count >= 18)
        {
            // Set the text value of your 'winText'
            winTextObject.SetActive(true);
            audioManager.PlaySFX(audioManager.levelUp);
            nextLevel.SetActive(true);
        }
    }
    // Restart the game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Next()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Return_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        // SceneManager.LoadScene("Menu"); // Replace "MenuScene" with the name of your menu scene
    }
}
