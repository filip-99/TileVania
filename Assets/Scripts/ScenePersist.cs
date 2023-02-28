using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    //[SerializeField] GameObject pickups;
    //[SerializeField] GameObject enemies;

    // Awake metoda je ista kao kod skripte GameSession. Dakle pravimo singlton od klase
    // Dakle kreiramo singlton klase koji kaže da u koliko imamo objekat sesije ne treba nam novi "resetovani"
    void Awake()
    {
        int numScenePersists = FindObjectsOfType<ScenePersist>().Length;
        if (numScenePersists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetGamePersist()
    {
        Destroy(gameObject);
    }
}
