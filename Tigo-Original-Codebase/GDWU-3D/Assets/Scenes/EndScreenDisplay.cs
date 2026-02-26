using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // NEEDED FOR REPLAY BUTTON

public class EndScreenDisplay : MonoBehaviour
{
    public TextMeshProUGUI track1Text;
    public TextMeshProUGUI track2Text;
    public TextMeshProUGUI track3Text;

    void Start()
    {
        // Display the times
        track1Text.text = "Track 1: " + FormatTime(PlayerPrefs.GetFloat("Track1Time"));
        track2Text.text = "Track 2: " + FormatTime(PlayerPrefs.GetFloat("Track2Time"));
        track3Text.text = "Track 3: " + FormatTime(PlayerPrefs.GetFloat("Track3Time"));
    }

    // THIS IS THE NEW FUNCTION FOR THE BUTTON
    public void OnReplayPressed()
    {
        // Loads the scene with index 0 (usually your main menu or first track)
        // Make sure your scenes are in the "Build Settings" list!
        SceneManager.LoadScene(0); 
    }

    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60F);
        int seconds = Mathf.FloorToInt(time % 60F);
        int milliseconds = Mathf.FloorToInt((time * 100F) % 100F);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}