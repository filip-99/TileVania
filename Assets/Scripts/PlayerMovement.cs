using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Potrebna je promenjiva koja će imati vrednost 1 ili -1 kada igrač pritisne levo ili desno
    Vector2 moveInput;

    void Start()
    {

    }

    void Update()
    {

    }
    
    // Komponenta Player Input sadrži polje actions, koje referencira na parametar koji ima sačuvane kontrole
    void OnMove(InputValue value)
    {
        //  Kada pritisnemo neko od dugmeta za kretanje, dobijamo vrednost 1 ili -1 po x ili y osi
        moveInput = value.Get<Vector2>();
        // Debug.Log(moveInput);
    }
}
