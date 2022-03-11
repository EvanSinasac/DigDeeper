using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 15, 2021
 * BatController for the bat enemy movement behaviour
 * Bats can occasionnally spawn off level and fly towards the player
 * */

public class BatController : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float moveSpeed = 3f;
    public float targetDistance = 10f;

    Animator animator;

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
        animator = GetComponent<Animator>();
    }

    void Update() {
        //For slow enchantment
        if (slowed)
        {
            slowedTimer -= Time.deltaTime;
            if (slowedTimer < 0)
            {
                moveSpeed = 3f;
                slowed = false;
            }
        }
    }

    
    void FixedUpdate()
    {
        var pos = player.transform.position;
        pursuitV(pos);
        if (pos.x < transform.position.x)
        {
            animator.SetFloat("Move X", -0.5f);
        } else if (pos.y < transform.position.y)
        {
            animator.SetFloat("Move X", 0.5f);
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

        velocity *= moveSpeed;
        // transform.position += velocity * Time.deltaTime;
        rb2.velocity = Vector2.ClampMagnitude((rb2.velocity + velocity) * moveSpeed, moveSpeed);

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
}
