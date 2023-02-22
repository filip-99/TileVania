using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D myRigidBody;
    BoxCollider2D myBoxCollider;

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        myRigidBody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void RotatePlayer()
    {
        // transform.localScale = new Vector2(transform.localScale.x * (-1), 1f);

        // Mathf.Sign() - Vraća 1 u koliko je vrednost >= 0 ili vraća -1 u koliko je manja od 0
        // Na osnovu ove vrednosti skaliraće se igrač
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidBody.velocity.x)), 1f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        moveSpeed = -moveSpeed;
        RotatePlayer();
    }
}
