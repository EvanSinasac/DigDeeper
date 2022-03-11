using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 7, 2021
 * RatController for the Rat enemy movement behaviour
 * Rats will spawn with two holes in walls and run from one hole to the other very quickly, 
 * with a random amount of time between when it reaches the end hole and restarts at the start hole
 * */

public class RatController : MonoBehaviour
{

    //public GameObject ratSpawn;
    //public GameObject ratDespawn;
    public string[] number;
    public bool alive = true;
    public float speed = 6.0f;
    public Rigidbody2D rb;
    public float timeToRespawn;
    public float RespawnTime = 5.0f;
    public SpriteRenderer sr;
    public Vector2 spawn;

    Animator animator;

    public bool slowed = false;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
       /* number = this.name.Split(' ');
        if (number.Length > 1)
        {
            ratSpawn = GameObject.Find("Rat Spawn " + number[1]);
            ratDespawn = GameObject.Find("Rat Despawn " + number[1]);
        } else
        {
            ratSpawn = GameObject.Find("Rat Spawn");
            ratDespawn = GameObject.Find("Rat Despawn");
        }
        */

        spawn = this.transform.position;// = ratSpawn.transform.position;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        animator.SetBool("Running", true);
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
                speed = 6.0f;
                slowed = false;
            }
        }

        if (!alive)
        {
            timeToRespawn -= Time.deltaTime;
            animator.SetBool("Running", false);
            
        } 

        if (timeToRespawn < 0)
        {
            alive = true;
            //sr.enabled = true;
            animator.SetBool("Running", true);
            timeToRespawn = 1;
        }
    }

    private void FixedUpdate()
    {
        if (alive)
        {
            Vector2 position = rb.position;
            position.x = position.x + speed * Time.deltaTime;

            rb.MovePosition(position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Rat Despawn"))
        {
            alive = false;
            transform.position = spawn;// ratSpawn.transform.position;
            timeToRespawn = RespawnTime;
            sr.enabled = false;
        }
    }
}
