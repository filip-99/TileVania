using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    Rigidbody2D rigidbody2;

    [Range(0.0f, 2.0f)]
    [SerializeField] float gravityScale = 0f;

    private void Start()
    {
        rigidbody2 = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            rigidbody2.gravityScale = gravityScale;
        }
    }
}
