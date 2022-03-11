using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Evan Sinasac - 104571345 && Matthew Pizzo - 
 * Last Worked On: January 21, 2021
 * */

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float restartLevelDelay = 1f;
    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;
    public Vector2 lastMotionVector;

    Animator animator;
    Vector2 lookDirection = new Vector2(1, 0);

    public float defaultSpeed = 5f;
    public float swingSpeed;
    public bool swinging;

    public bool shopping;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        animator = GetComponent<Animator>();

        shopping = false;
        moveSpeed = 5f;

        //PlayerPrefs.SetFloat("speedPowerUp", 1);
    } //end of Starts

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(movement.x, movement.y);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (movement.x != 0 || movement.y != 0) {
            lastMotionVector = new Vector2(movement.x, movement.y).normalized;
        } //end of if

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        } //end of if

        if (!swinging && !shopping)
        {
            animator.SetFloat("Move X", lookDirection.x);
            animator.SetFloat("Move Y", lookDirection.y);
            animator.SetFloat("Speed", move.magnitude);
        }

        if (swinging)
        {
            if (swingSpeed <= 0)
            {
                swinging = false;
                moveSpeed = defaultSpeed;
            }
            else
                swingSpeed -= Time.deltaTime;
        } //end of if

    } //end of Update

    private void FixedUpdate()
    {
        //Movement
        // rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        
        Vector2 position = rb.position;//transform.position;
        position.x = position.x + moveSpeed * movement.x * Time.fixedDeltaTime * PlayerPrefs.GetFloat("speedPowerUp");
        position.y = position.y + moveSpeed * movement.y * Time.fixedDeltaTime * PlayerPrefs.GetFloat("speedPowerUp");
        //transform.position = position;
        rb.MovePosition(position);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;


    } //end of FixedUpdate

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the tag of the trigger collided with is Exit.
        if (other.tag == "Exit")
        {
            PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("currentLevel")+1);
            //Invoke the Restart function to start the next level with a delay of restartLevelDelay (default 1 second).
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

            //Disable the player object since level is over.
            enabled = false;
        }

    } //end of OnTriggerEnter2D

    public void swing (float speed)
    {
        if (!shopping)
        {
            swinging = true;
            moveSpeed = 0;
            swingSpeed = speed;
        }
    } //end of swing

    public bool getSwinging ()
    {
        return swinging;
    } //end of getSwinging

    public void Shopping (bool areWe)
    {
        shopping = areWe;

        if (shopping)
        {
            moveSpeed = 0;
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            moveSpeed = defaultSpeed;
        }

    } //end of Shopping

} //end of PlayerMovement
