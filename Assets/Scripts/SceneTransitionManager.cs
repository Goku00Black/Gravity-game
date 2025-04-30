using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance;

    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        // Singleton setup
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Preserve across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        // Start scene with a fade-in from black
        if (fadeImage != null)
            StartCoroutine(Fade(1f, 0f));
    }

    public void LoadSceneWithFade(string sceneName)
    {
        if (fadeImage != null)
            StartCoroutine(FadeOutAndLoad(sceneName));
        else
            SceneManager.LoadScene(sceneName); // fallback
    }

    IEnumerator FadeOutAndLoad(string sceneName)
    {
        yield return Fade(0f, 1f); // Fade to black
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (fadeImage != null)
            StartCoroutine(Fade(1f, 0f)); // Fade in from black
    }

    IEnumerator Fade(float from, float to)
    {
        float time = 0f;
        Color color = fadeImage.color;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(from, to, time / fadeDuration);
            color.a = alpha;
            fadeImage.color = color;
            yield return null;
        }

        color.a = to;
        fadeImage.color = color;
    }
}
