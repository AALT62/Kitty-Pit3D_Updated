using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // Variables for UI and movement
    public float speed;
    public float startingTime;
    public TMP_Text countText;
    public TMP_Text winText;
    public TMP_Text timeText;
    public TMP_Text FinalTimer;
    public TMP_Text finalTimeText; // Reference to the new FinalTimeText component
    
    public GameObject youWinPanel;

    public AudioClip coinSFX;   // Sound for collecting coins
    public AudioClip deathSFX;  // Sound for resetting the scene
    private AudioSource audioSource;

    private Rigidbody rb;
    private int count;
    private bool gameOver;
    private bool isResetting = false; // Prevent multiple resets
    public static float finalElapsedTime; // Static variable to store the final time

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        SetCountText();
        winText.text = "";
        startingTime = Time.time;
        gameOver = false;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (gameOver)
            return;

        float timer = Time.time - startingTime;
        timeText.text = "Elapsed Time: " + ((int)timer / 60).ToString() + ":" + (timer % 60).ToString("f0");

        
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);
    }
    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
        if (count == 10)
        {
            gameOver = true;
            winText.text = "You win!";
            speed = 0;
            SceneManager.LoadScene("WIN");
            rb.isKinematic = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            audioSource.clip = coinSFX;
            audioSource.Play();
            StartCoroutine(FlashColor());
        }

        if (other.gameObject.CompareTag("DeathZone") && !isResetting)
        {
            isResetting = true; // Prevent further resets
            audioSource.clip = deathSFX; // Ensure deathSFX is assigned
            audioSource.Play(); // Play the sound effect
            StartCoroutine(TurnRedBeforeReset());
            StartCoroutine(ResetSceneAfterDelay(1.0f)); // Adjust delay as necessary
        }

       



        if (other.gameObject.CompareTag("Shrink"))
        {
            if (transform.localScale.x >= 0.5f)
            {
                transform.localScale *= 0.75f;     // decreases scale by 25%
            }
        }

        if (other.gameObject.CompareTag("Grow"))
        {
            if (transform.localScale.x <= 2.0f)
            {
                transform.localScale *= 1.25f;    // increase scale by 25%
            }
        }

       
    }
    private IEnumerator TurnRedBeforeReset()
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        Color originalColor = playerRenderer.material.color;

        // Set player color to red
        playerRenderer.material.color = Color.red;

        // Wait for a brief moment before resetting (adjust delay as needed)
        yield return new WaitForSeconds(1f);

        // Reset the scene
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);

        // Reset color to original after scene reload
        playerRenderer.material.color = originalColor;
        isResetting = false; // Reset the flag for future use
    }
    private IEnumerator ResetSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        isResetting = false; // Reset the flag for future use
    }

    

    private IEnumerator FlashColor()
    {
        Renderer playerRenderer = GetComponent<Renderer>();
        Color originalColor = playerRenderer.material.color;

        float flashDuration = 2f; // Duration of the flash effect
        float flashInterval = 0.1f; // Interval between color changes
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            playerRenderer.material.color = Color.blue;
            yield return new WaitForSeconds(flashInterval);
            playerRenderer.material.color = Color.yellow;
            yield return new WaitForSeconds(flashInterval);
            elapsed += flashInterval * 2;
        }

        // Revert to original color
        playerRenderer.material.color = originalColor;



    }
}
