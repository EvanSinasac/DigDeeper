using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 22, 2021
 * GolemController for the Golem enemy movement behaviour
 * Golems sleep disguised as rocks until the player gets close enough 
 * */

public class GolemController : MonoBehaviour
{

    public GameObject player;
    Animator animator;
    public float distance;
    public float distanceToWake = 4f;
    public bool awake = false;
    public float timer;
    public float timeToAwake = 2.5f;
    public float timeToSleep = 1.5f;
    public float speed = 0.5f;

    public bool slowed = false;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(this.transform.position, player.transform.position);
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
                speed = 0.5f;
                slowed = false;
            }
        }

        distance = Vector3.Distance(this.transform.position, player.transform.position);

        if (awake)
        {
            if (timer < 0)
            {
                seek(player.transform.position);
                this.gameObject.layer = 10;
            } else
                timer -= Time.deltaTime;

            if (player.transform.position.y > this.transform.position.y)
            {
                animator.SetFloat("Up", 1);
            }
            else
                animator.SetFloat("Up", 0);
        }
    }

    private void FixedUpdate()
    {
        if (distance < distanceToWake)
        {
            if (!awake)
            {
                awake = true;
                animator.SetTrigger("WakeUp");
                timer = timeToAwake;
            }
        }
        else
        {
            if (awake)
            {
                awake = false;
                animator.SetTrigger("GoToSleep");
                this.gameObject.layer = 14;
            }
        }
    }

    void seek(Vector3 pos)
    {
        //implement the code for seek behavior
        //to seek the player 
        Vector2 velocity;
        Rigidbody2D rb2 = GetComponent<Rigidbody2D>();

        velocity = pos - transform.position;
        velocity = velocity.normalized;

        velocity *= speed;
        //transform.position += velocity * Time.deltaTime;
        rb2.velocity = Vector2.ClampMagnitude((rb2.velocity + velocity) * speed, speed);

    } //end of seek
}
