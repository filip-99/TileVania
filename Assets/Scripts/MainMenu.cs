using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public string startScene;

    void Start()
    {
        // Prona�i sve objekte sa skriptom GameSession u sceni Main Menu
        GameSession[] gameSessions = FindObjectsOfType<GameSession>();

        // Uni�ti svaki prona�eni objekat
        foreach (GameSession gameSession in gameSessions)
        {
            Destroy(gameSession.gameObject);
        }
        Debug.Log("Jeste");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(startScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
