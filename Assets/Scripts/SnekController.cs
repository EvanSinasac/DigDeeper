using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 22, 2021
 * SnekController for the Snek enemy movement behaviour
 * Sneks can only move horizontally or vertically at a time and charge at the player if they come in line with them
 * */

public class SnekController : MonoBehaviour
{

    public GameObject player;
    Rigidbody2D rb;
    public int direction = 0;
    public float moveSpeed = 5f;
    public float chargeSpeed = 2f;
    public bool seePlayer = false;
    public bool charging = false;
    public bool firstSee = false;
    public float changeDirectionTime = 2f;
    public float chargeTime = 4f;
    public Vector2 moveDir = new Vector2 (0,0);
    public Vector2 chargeDir = new Vector2(0, 0);

    public Vector2 lookDirection = new Vector2(0,0);

    Animator animator;

    public bool slowed = false;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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

        if (!seePlayer && !charging)
        {
            wander();
            lookDirection.Set(moveDir.x, moveDir.y);
        } else
        {
            if (!firstSee)
            {
                firstSee = true;
                chargeDirection();
            }
            charging = true;
        }

        if (charging)
        {
            charge();
            chargeTime -= Time.deltaTime;
            lookDirection.Set(chargeDir.x, chargeDir.y);
        }

        if (chargeTime < 0)
        {
            chargeTime = 4f;
            charging = false;
            firstSee = false;
        }

        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(player.transform.position.x - transform.position.x) < 1 || Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
        {
            seePlayer = true;
        } 
        else
        {
            seePlayer = false;
        }
    }

    void wander()
    {
        if (changeDirectionTime < 0)
        {
            direction = Random.Range(0, 4);
            changeDirectionTime = 2f;
            switch(direction)
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
        } else
        {
            changeDirectionTime -= Time.deltaTime;

            rb.velocity = Vector2.ClampMagnitude(moveDir * moveSpeed, 1);
        }
        
    }

    void charge ()
    {
        rb.velocity = Vector2.ClampMagnitude(chargeDir * chargeSpeed * moveSpeed, 3);
    }

    void chargeDirection ()
    {
        chargeDir = player.transform.position - transform.position;
        if (Mathf.Abs(chargeDir.x) < Mathf.Abs(chargeDir.y))
        {
            chargeDir.x = 0;
            chargeDir.Normalize();
        } else
        {
            chargeDir.y = 0;
            chargeDir.Normalize();
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (charging)
        {
            if (collision.tag != "Player")
            {
                charging = false;
                firstSee = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (charging)
        {
            if (collision.tag != "Player")
            {
                charging = false;
                firstSee = false;
            }
        }
    }
}
