using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{

    public float changeDirectionTime = 7f;
    int direction = 0;
    Vector2 moveDir;
    public float moveSpeed = 0.5f;
    Rigidbody2D rb;

    Vector2 lookDirection = new Vector2(0, 0);
    float speed = 1;

    Animator animator;
    EnemyController ec;

    public float collapsedTimer = 5f;

    public bool slowed = false;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ec = GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        //For slow enchantment
        if (slowed)
        {
            slowedTimer -= Time.deltaTime;
            if (slowedTimer < 0)
            {
                moveSpeed = 5f;
                slowed = false;
            }
        }

        if (ec.getHealth() <= 0)
        {
            speed = 0f;
            rb.velocity = Vector2.zero;
            if (collapsedTimer < 0)
            {
                ec.setHealth(30);
                ec.skellyDown = false;
            }
            else
                collapsedTimer -= Time.deltaTime;

        } else
        {
            collapsedTimer = 7f;
            wander();
            lookDirection.Set(moveDir.x, moveDir.y);
            speed = 1f;
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        animator.SetFloat("Speed", speed);

    }


    void wander()
    {
        if (changeDirectionTime < 0)
        {
            direction = Random.Range(0, 4);
            changeDirectionTime = 2f;
            switch (direction)
            {
                case 0: //up
                    moveDir = new Vector2(0, 1);
                    break;
                case 1: //down
                    moveDir = new Vector2(0, -1);
                    break;
                case 2: //left
                    moveDir = new Vector2(-1, 0);
                    break;
                case 3: //right
                    moveDir = new Vector2(1, 0);
                    break;
                default:
                    break;
            }
        }
        else
        {
            changeDirectionTime -= Time.deltaTime;

            rb.velocity = Vector2.ClampMagnitude(moveDir * moveSpeed, 1);
        }

    }
}
