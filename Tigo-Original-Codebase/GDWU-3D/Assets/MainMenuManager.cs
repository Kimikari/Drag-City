using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        // Change "Track1" to the exact name of your first racing level
        SceneManager.LoadScene("Track1"); 
    }
}