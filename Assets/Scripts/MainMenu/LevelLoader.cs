using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TextMeshProUGUI progressText;

    public void LoadMainMenuScene()
    {
        int sceneIndex = 0;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadLevel()
    {
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadAsynchronously(sceneIndex+1));
    }

    public void LoadWinScene()
    {
        int sceneIndex = 2;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LoadLoseScene()
    {
        int sceneIndex = 3;
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.SetActive(true);

        while(!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;
            progressText.text = progress * 100 + "%";

            yield return null;
        }
    }
}
