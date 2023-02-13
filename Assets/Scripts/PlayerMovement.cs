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

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Run();
    }

    // Komponenta Player Input sadrži polje actions, koje referencira na parametar koji ima sačuvane kontrole
    void OnMove(InputValue value)
    {
        //  Kada pritisnemo neko od dugmeta za kretanje, dobijamo vrednost 1 ili -1 po x ili y osi
        moveInput = value.Get<Vector2>();
        // Debug.Log(moveInput);
    }

    // Metoda za kretanje igrača
    private void Run()
    {
        // Potrebno je kretanje ograničiti samo na x osu, dakle horizontalno
        Vector2 playerVelocity = new Vector2(moveInput.x * moveSpeed, myRigidbody.velocity.y);
        // Pomeramo kruto telo
        myRigidbody.velocity = playerVelocity;
    }

}
