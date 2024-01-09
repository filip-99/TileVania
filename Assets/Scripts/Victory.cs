using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Victory : MonoBehaviour
{
    public string mainMenu;

    void Start()
    {
        // Prona�i sve objekte sa skriptom GameSession u sceni Victory
        GameSession[] gameSessions = FindObjectsOfType<GameSession>();

        // Uni�ti svaki prona�eni objekat
        foreach (GameSession gameSession in gameSessions)
        {
            Destroy(gameSession.gameObject);
        }

        ScenePersist[] scenePersists = FindObjectsOfType<ScenePersist>();
        foreach (ScenePersist scenePersistss in scenePersists)
        {
            Destroy(scenePersistss.gameObject);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }
}
