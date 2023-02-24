using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;

    void Awake()
    {
        // Dobijamo broj objekta u sceni koji imaju ovu skriptu
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // U koliko više od 1 objekta ima onda poništavamo sve objekte do vrednost ne bude 1
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    private void TakeLife()
    {
        playerLives--;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResetGameSession()
    {
        SceneManager.LoadScene(0);
        // Kada igrač pogine važno je da uništimo ovaj objekat sesije, jer će se kreirati novi prilikom učitavanja scene
        Destroy(gameObject);
    }

}
