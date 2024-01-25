using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGolem : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool movingRight;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;


    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        leftPoint.parent = null;
        rightPoint.parent = null;
        movingRight = true;
    }

    void Update()
    {
        if (movingRight)
        {
            theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);

            theSR.flipX = true;

            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }

        else
        {

            theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);

            theSR.flipX = false;

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }

    }
}
