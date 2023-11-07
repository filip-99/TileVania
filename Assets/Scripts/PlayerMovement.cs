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
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    BoxCollider2D myBoxCollider;
    GameSession gameSession;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpSpeed;

    [SerializeField] float climbSpeed;
    float gravityScaleAtStart;

    [SerializeField] Vector2 bounceForce = new Vector2(20f, 20f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    bool isAlive = true;
    bool canDoubleJump;
    bool isFired;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();

        gameSession = FindObjectOfType<GameSession>();

        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    void Update()
    {
        if (!isAlive || FindObjectOfType<LevelExit>().isPause) { return; }
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    // Komponenta Player Input sadrži polje actions, koje referencira na parametar koji ima sačuvane kontrole
    void OnMove(InputValue value)
    {
        // U koliko je igrač mrtav izađi iz cele metode
        if (!isAlive) { return; }
        //  Kada pritisnemo neko od dugmeta za kretanje, dobijamo vrednost 1 ili -1 po x ili y osi, a po difoltu se setuje na 0
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (!isAlive) { return; }

        if (!myBoxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            if (canDoubleJump && value.isPressed)
            {
                canDoubleJump = false;
                myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpSpeed / 2);
            }
            // U koliko ne dodiirujemo zemlju izaćiće iz cele metode
            return;
        }
        else
        {
            canDoubleJump = true;
        }

        if (value.isPressed)
        {
            myRigidbody.velocity += new Vector2(myRigidbody.velocity.x, jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive || FindObjectOfType<LevelExit>().isPause) { return; }

        if (value.isPressed)
        {
            StartCoroutine(AttackAction());

        }
    }

    IEnumerator AttackAction()
    {
        myAnimator.SetTrigger("isFired");
        yield return new WaitForSeconds(0.12f);
        // Pritiskom na neku od dugmeta za pucanje instanciraćemo referenciran objekat iz prefaba
        Instantiate(bullet, gun.position, transform.rotation);
    }

    // Metoda za kretanje igrača
    private void Run()
    {
        // Potrebno je kretanje ograničiti samo na x osu, dakle horizontalno
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        // Pomeramo kruto telo
        myRigidbody.velocity = playerVelocity;

        // Promenjiva playerHorizontalSpeed biće true ako je uslov ispunjen
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

    // Potrebna je metoda za penjanje uz merdevine, koja je slična kao za trčanje samo se gleda y osa
    private void ClimbLadder()
    {
        // U koliko nema dodira sa merdevinama izvrši sledeće
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myAnimator.SetBool("isClimbing", false);
            myRigidbody.gravityScale = gravityScaleAtStart;
            // Ako nema dodira odmah izađi iz cele metode ClimbLadder()
            return;
        }

        myRigidbody.gravityScale = 0f;
        Vector2 climbVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;

        // U koliko je brzina po y osi > ili < od "0" izvršiće se animacija kretanja
        bool playerVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerVerticalSpeed);

    }

    // Kada igrač pogine izvršiće se sledeća metoda
    private void Die()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            myAnimator.SetTrigger("Dying");
            myRigidbody.velocity = bounceForce;
            isAlive = false;

            // Pri zadobijanju udarca pozivamo metodu iz skripte GameSession
            gameSession.ProcessPlayerDeath();
        }
    }

}
