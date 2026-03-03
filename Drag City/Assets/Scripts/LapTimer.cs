using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // We need this to change levels!

// Keeps track of the time it take to finish each track
public class LapTimer : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI timerText;

    [Header("Level Transition")]
    public string nextSceneName = "Track2"; // Name of your second scene
    public string timeSaveKey = "Track1Time"; // The secret ID used to save this specific time

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
                SceneManager.LoadScene(nextSceneName);
            }

        }
    }
}
