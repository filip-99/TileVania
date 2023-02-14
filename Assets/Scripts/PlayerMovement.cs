using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Potrebna je promenjiva koja će imati vrednost 1 ili -1 kada igrač pritisne levo ili desno
    Vector2 moveInput;

    Rigidbody2D myRigidbody;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    Animator myAnimator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Run();
        FlipSprite();
    }

    // Komponenta Player Input sadrži polje actions, koje referencira na parametar koji ima sačuvane kontrole
    void OnMove(InputValue value)
    {
        //  Kada pritisnemo neko od dugmeta za kretanje, dobijamo vrednost 1 ili -1 po x ili y osi
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    // Metoda za kretanje igrača
    private void Run()
    {
        // Potrebno je kretanje ograničiti samo na x osu, dakle horizontalno
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        // Pomeramo kruto telo
        myRigidbody.velocity = playerVelocity;

        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        // Pokrenućemo triger koji tera igrača da pređe u stanje trčanja
        myAnimator.SetBool("isRunning", playerHorizontalSpeed);

    }

    // Potrebno je flipovati sprajt igrača tokom kretanja
    private void FlipSprite()
    {
        // playerHorizontalSpeed = true, ako je brzina igrača veća od Epsilon(0)
        // 0 može da napravi problem, jer nekad može doći do zabune, jer se može desiti da se sa 0 poredi broj 0.00000000000001 i dolazi do zabune
        bool playerHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        // Ograničavamo da igrač se objekat igrača ne flipuje ako krenemo na levo
        if (playerHorizontalSpeed)
        {

            // Mathf.Sign() - Vraća 1 u koliko je vrednost >= 0 ili vraća -1 u koliko je 
            // Na osnovu ove vrednosti skaliraće se igrač
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

}
