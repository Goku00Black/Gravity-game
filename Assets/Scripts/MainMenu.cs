using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectPanel; // Assign in Inspector
    public Button playButton;           // Assign in Inspector
    public Button quitButton;

    void Start()
    {

        // Hide level select initially
        if (levelSelectPanel != null)
            levelSelectPanel.SetActive(false);
    }

    public void OnPlayButton()
    {
        levelSelectPanel.SetActive(true);     // Show Level Select
        playButton.interactable = false;      // Disable buttons
        quitButton.interactable = false;
    }

    public void OnLevel1()
    {
        SceneManager.LoadScene("Endless");
    }

    public void OnLevel2()
    {
        SceneManager.LoadScene("LocalPlayer");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void OnQuit()
    {
        Debug.Log("Quit Game");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

