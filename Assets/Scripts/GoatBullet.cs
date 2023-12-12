using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoatBullet : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed;
    Goat goat;
    float xSpeed;
    PlayerMovement player;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        goat = FindObjectOfType<Goat>();
        xSpeed = -goat.transform.localScale.x * bulletSpeed;
        player = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, 0f);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.Die();
        }
        Destroy(gameObject);
    }
}
