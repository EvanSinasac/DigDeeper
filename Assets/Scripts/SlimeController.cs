using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 22, 2021
 * SlimeController for the Slime enemy movement behaviour
 * Slimes will meander unless the player is close enough, then they will target the player
 * */

public class SlimeController : MonoBehaviour
{
    
    public GameObject player;
    public float distance;
    public float moveSpeed = 1f;
    public float targetDistance = 10f;
    public float meanderTime;
    Vector2 wayPoint = new Vector3(0, 0, 0);
    Vector2 lookDirection = new Vector2(0, -1);
    Animator animator;
    Rigidbody2D rb2;

    public bool slowed = false;
    public float slowedTimer;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        meanderTime = 5f;
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();
    } //end of Start

    // Update is called once per frame
    void Update()
    {
        //For slow enchantment
        if (slowed)
        {
            slowedTimer -= Time.deltaTime;
            if (slowedTimer < 0)
            {
                moveSpeed = 1f;
                slowed = false;
            }
        }

        distance = Vector3.Distance(this.transform.position, player.transform.position);
        var pos = player.transform.position;
        if (distance < targetDistance)
        {
            pursuitV(pos);
        } else
        {
            if (meanderTime <= 0)
            {
                meander();
                meanderTime = 5f;
            } //end of if
            meanderTime -= Time.deltaTime;
            //transform.position.x = transform.position.x + wayPoint.x * moveSpeed * Time.deltaTime;
            //transform.position += wayPoint * moveSpeed * Time.deltaTime;

            rb2.velocity = Vector2.ClampMagnitude((rb2.velocity + wayPoint) * moveSpeed, moveSpeed);

            lookDirection.Set(wayPoint.x, wayPoint.y);
            lookDirection.Normalize();
            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);
        } //end of else
    } //end of Update

    void seek(Vector3 pos)
    {
        //implement the code for seek behavior
        //to seek the player 
        Vector2 velocity;
        //Rigidbody2D rb2 = GetComponent<Rigidbody2D>();

        velocity = pos - transform.position;
        velocity = velocity.normalized;
        
        velocity *= moveSpeed;
        //transform.position += velocity * Time.deltaTime;
        //rb2.AddForce(velocity);// * Time.deltaTime);

        rb2.velocity = Vector2.ClampMagnitude((rb2.velocity + velocity) * moveSpeed, moveSpeed);

        //Debug.Log(velocity);

        lookDirection.Set(velocity.x, velocity.y);
        lookDirection.Normalize();
        animator.SetFloat("Move X", lookDirection.x);
        animator.SetFloat("Move Y", lookDirection.y);
        //rb2.AddForce(velocity * Time.deltaTime);

    } //end of seek

    void pursuitV(Vector3 pos)
    {
        //implement the code for Pursuit seek behavior
        Vector3 velocity = Vector3.zero, predictedLocation, targetVelocity;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();//, rb2 = GetComponent<Rigidbody2D>();
        targetVelocity = rb.velocity;
        float T = Mathf.Abs((pos - transform.position).magnitude) / moveSpeed;

        predictedLocation = pos + targetVelocity * T;

        seek(predictedLocation);
    } //end of pursuit

    void meander ()
    {
        //meander behaviour for when the player isn't close enough
        wayPoint = (Random.insideUnitCircle * 360).normalized;
        rb2.velocity = new Vector2(0, 0);
        // transform.LookAt(wayPoint);
        //transform.position += transform.TransformDirection(Vector2.right) * moveSpeed * Time.deltaTime;
    } //end of meander
    
} //end of SlimeController
