using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; 

public class LapTimer : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;

    [Header("Level Transition & Limits")]
    public string timeSaveKey = "Track1Time"; 
    public float maxTimeLimit = 60f; // NEW: The time limit in seconds (e.g., 60 seconds)

    private float currentTime = 0f;
    private bool isTiming = false;
    private bool hasStarted = false;
    private bool hasFinished = false;

    void Start()
    {
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isTiming)
        {
            currentTime += Time.deltaTime;
            UpdateTimerDisplay();

            // NEW: Check if the player exceeded the time limit
            if (currentTime >= maxTimeLimit)
            {
                RestartGame();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime % 60F);
        int milliseconds = Mathf.FloorToInt((currentTime * 100F) % 100F);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Make sure the object crossing the line has the tag "Player"
        if (other.CompareTag("Player"))
        {
            // FIRST PASS: Start the race
            if (!hasStarted)
            {
                hasStarted = true;
                isTiming = true;
                Debug.Log("Line Crossed: Race Started!");
            }
            // SECOND PASS: Finish the race, save, and load next track
            else if (hasStarted && !hasFinished)
            {
                hasFinished = true;
                isTiming = false;

                // 1. Save the final time using PlayerPrefs
                PlayerPrefs.SetFloat(timeSaveKey, currentTime);
                PlayerPrefs.Save();

                // 2. Load the next scene
                Debug.Log("Time Saved! Loading next track...");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    // NEW: Reloads the current scene to restart the game
    private void RestartGame()
    {
        Debug.Log("Time ran out! Restarting level...");
        // This gets the name of the scene you are currently in and reloads it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }
}