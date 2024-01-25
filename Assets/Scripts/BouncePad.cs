using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    private Animator anim;
    public float bounceForce = 20f;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerMovement.instance.myRigidbody.velocity = new Vector2 (PlayerMovement.instance.myRigidbody.velocity.x, bounceForce);
            anim.SetTrigger("Bounce");
        }
    }
}
