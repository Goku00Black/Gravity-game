using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public GameObject levelSelectPanel; // Assign in Inspector
    public Button playButton;                   // Assign in Inspector
    public Button quitButton;

    public void OnPlayButton()
    {
        levelSelectPanel.SetActive(true); // Show Level 1 & Level 2 buttons
        playButton.interactable = false;        // Disable Play
        quitButton.interactable = false;
    }

    public void OnLevel1()
    {
        SceneManager.LoadScene("Endless"); // Make sure the scene name matches exactly
    }

    public void OnLevel2()
    {
        SceneManager.LoadScene("LocalPlayer");
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
