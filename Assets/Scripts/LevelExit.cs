using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    // Metoda koja pre izvršavanja ima definisano vreme čekanja
    IEnumerator LoadNextLevel()
    {
        // WaitForSecondsRealtime vree čekanja u sekundama (realno vreme)
        yield return new WaitForSecondsRealtime(levelLoadDelay);

        int nexSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nexSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nexSceneIndex = 0;
        }

        FindObjectOfType<ScenePersist>().ResetGamePersist();
        SceneManager.LoadScene(nexSceneIndex);
    }
}
