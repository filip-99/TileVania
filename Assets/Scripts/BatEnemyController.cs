using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemyController : MonoBehaviour
{
    public Transform[] points;
    public float moveSpeed;
    private int currentPoint;
    public SpriteRenderer theSR;
    public float distanceToAttackPlayer;
    public float chaseSpeed;
    private Vector3 attackTarget;

    void Start()
    {
        theSR = GetComponentInChildren<SpriteRenderer>();
        for (int i = 0; i < points.Length; i++)
        {
            points[i].parent = null;
        }
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, PlayerMovement.instance.transform.position) > distanceToAttackPlayer)
        {
            attackTarget = Vector3.zero;

            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.5f)
            {
                currentPoint++;

                if (currentPoint >= points.Length)
                {
                    currentPoint = 0;
                }
            }

            if (transform.position.x < points[currentPoint].position.x)
            {
                theSR.flipX = true;
            }
            else if (transform.position.x > points[currentPoint].position.x)
            {
                theSR.flipX = false;
            }
        }
        else
        {
            if(attackTarget == Vector3.zero)
            {
                attackTarget = PlayerMovement.instance.transform.position;
            }
            transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);
        }
    }
}
