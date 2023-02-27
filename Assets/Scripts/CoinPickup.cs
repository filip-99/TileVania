using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    // Broj poena koji se dodaje skoru za prikupljen novčić
    [SerializeField] int numberPoints = 5;

    // Da ne dođe do greške da 2 puta prikupimo novčić potreban je prekidač
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().IncreaseScore(numberPoints);
            AudioManager.instance.PlaySFX(0);
            // Još jedna sigurnost da smo pokupili novčić je da isključimo objekat
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
