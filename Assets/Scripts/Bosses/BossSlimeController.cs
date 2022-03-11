using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSlimeController : MonoBehaviour
{

    public GameObject player;
    public float distance;
    public float moveSpeed = 5f;
    public float targetDistance = 10f;
    Vector2 wayPoint = new Vector3(0, 0, 0);
    Vector2 lookDirection = new Vector2(0, -1);
    public Animator animator;
    Rigidbody2D rb2;

    public BossHealthBar bossHealthBar;

    public bool slowed = false;
    public float slowedTimer;

    public GameObject exitMine;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        distance = Vector3.Distance(this.transform.position, player.transform.position);
        animator = GetComponent<Animator>();
        rb2 = GetComponent<Rigidbody2D>();

        bossHealthBar = FindObjectOfType<BossHealthBar>();
        bossHealthBar.SetMaxHealth(500);
        bossHealthBar.SetHealth((int)gameObject.GetComponent<EnemyController>().getHealth());
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

        distance = Vector3.Distance(this.transform.position, player.transform.position);
        var pos = player.transform.position;
        
        pursuitV(pos);
        
    } //end of Update

    private void FixedUpdate()
    {
        bossHealthBar.SetHealth((int)gameObject.GetComponent<EnemyController>().getHealth());
    }

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

        rb2.velocity = Vector2.ClampMagnitude(rb2.velocity + velocity, 1);

        //Debug.Log(velocity);

        lookDirection.Set(velocity.x, velocity.y);
        lookDirection.Normalize();
        animator.SetFloat("Speed", rb2.velocity.magnitude);
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


    private void OnDestroy()
    {
        Instantiate(exitMine, this.transform.position, Quaternion.identity);
        GameObject.FindGameObjectWithTag("BossUI").SetActive(false);
    }

} //end of BossSlimeController
