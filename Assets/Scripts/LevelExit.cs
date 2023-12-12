using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 3f;
    public GameObject pauseScreen;
    public bool isPause;

    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] StorySO[] story = new StorySO[2];

    private void Start()
    {
        storyText.text = story[SceneManager.GetActiveScene().buildIndex - 1].GetStory();
    }

    public void PauseUnpause()
    {
        if (isPause)
        {
            isPause = false;
            pauseScreen.SetActive(false);
            // Setujemo normalno vreme u igri
            Time.timeScale = 1f;
        }

        else
        {
            isPause = true;
            pauseScreen.SetActive(true);
            // Zaustavljamo vreme u igri
            Time.timeScale = 0f;
        }
    }

    // Metoda koja pre izvršavanja ima definisano vreme čekanja
    public void LoadNextLevel()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    public void LoadMainMenu()
    {
        StartCoroutine(LoadMainMenuRoutine());
    }

    private IEnumerator LoadSceneRoutine()
    {
        UIController.instance.FadeToBlack();

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        FindObjectOfType<ScenePersist>().ResetGamePersist();
        SceneManager.LoadScene(nextSceneIndex);

        PauseUnpause();

    }

    private IEnumerator LoadMainMenuRoutine()
    {
        UIController.instance.FadeToBlack();

        yield return new WaitForSecondsRealtime(levelLoadDelay);

        SceneManager.LoadScene("Main Menu");

        PauseUnpause();

    }
}
