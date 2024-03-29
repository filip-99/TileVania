﻿using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{

    [SerializeField] int playerLives = 3;
    int scoreCounter = 0;


    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;

    void Awake()
    {
        // Dobijamo broj objekta u sceni koji imaju ovu skriptu
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // Kada prvi put učitamo scenu imaćemo 1 objekat sa ovom skriptom, koji pamti pokupljene novčiće, skor, živote...
        // Pošto imamo ovaj objekat igre za zapamćenim vrednostima i kada poginemo ne želimo da ga resetujemo tj. da kreiramo novi. Tako da će mo novi objekat igre uništiti
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);

            livesText.text = playerLives.ToString();
            scoreText.text = scoreCounter.ToString();
        }
    }

    void Start()
    {
        
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives >= 1)
        {
            TakeLife();
        }
        else
        {
            ResetGameSession();
        }
    }

    public void IncreaseScore(int numberPoints)
    {
        scoreCounter += numberPoints;
        scoreText.text = scoreCounter.ToString();
    }

    private void TakeLife()
    {
        playerLives -= 1;
        Debug.Log(playerLives.ToString());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        livesText.text = playerLives.ToString();
    }

    private void ResetGameSession()
    {
        FindObjectOfType<ScenePersist>().ResetGamePersist();
        SceneManager.LoadScene(0);
        // Kada igrač pogine važno je da uništimo ovaj objekat sesije, jer će se kreirati novi prilikom učitavanja scene
        // Dakle kada bude Game Over potrebno je resetovati sve vrednosti i krenuti ispočetka
        Destroy(gameObject);
    }



}