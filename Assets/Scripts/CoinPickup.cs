using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    int coinCounter;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            coinCounter++;
            Destroy(gameObject);
        }
    }
}
