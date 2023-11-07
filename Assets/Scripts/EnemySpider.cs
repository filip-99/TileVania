using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpider : MonoBehaviour
{
    public float moveSpeed;
    public Transform leftPoint;
    public Transform rightPoint;
    private bool movingRight;
    private Rigidbody2D theRB;
    public SpriteRenderer theSR;
    public float moveTime;
    public float waitTime;
    private float moveCount;
    private float waitCount;
    private Animator anim;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();

        leftPoint.parent = null;
        rightPoint.parent = null;

        movingRight = true;
        moveCount = moveTime;

        anim = GetComponent<Animator>();

    }

    void Update()
    {
        if (moveCount > 0)
        {
            moveCount -= Time.deltaTime;
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


            if (moveCount <= 0)
            {
                anim.speed = 0f;
                // Generisacemo random vreme cekanja koje ce da varira u opsegu zadatog vremena pomnozenim sa 0.75 i zatadok vremena pomnozenim sa 1.25
                waitCount = Random.Range(waitTime * 0.75f, waitTime * 1.25f);
            }
            // anim.SetBool("isMoving", true);
        }

        else if (waitCount > 0)
        {
            // Oduzimaju sesekunde
            waitCount -= Time.deltaTime;
            // Zaustavicemo kruto telo, da bi sacekali vreme da istekne
            theRB.velocity = new Vector2(0F, theRB.velocity.y);
            // Kada brojac dodje do 0
            if (waitCount <= 0)
            {
                anim.speed = 1f;
                //// Brojac vremena opet uzima zadatu vrednost u Unity-u i krece se ispocetka tj. prvi uslov je ispunjen
                // Takodje i ovde generisemo random vreme cekanja koje je u opsegu
                moveCount = Random.Range(moveTime * 0.75f, waitTime * 0.75f);
            }
            // Kada zaba stoji u mestu aktivira ce animacija stajanja
            // anim.SetBool("isMoving", false);
        }
    }
}
