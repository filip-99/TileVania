using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        // Pristupamo parametru x na scale transformaciji igrača da bi na osnovu strane na koju je okrenut usmerili metak koji igrač ispaljuje
        // Kada se instancira objekat odmah će se pokrenuti start metoda i xSpeed će uzeti transformaciju igrača tj. scale po x
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(xSpeed, myRigidBody.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
