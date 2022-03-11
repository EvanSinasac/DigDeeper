using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 22, 2021
 * MoleController for the Mole enemy movement behaviour
 * Moles dig up from underground to attack the player, attracted by the tremors caused by the player walking around
 * */

public class MoleController : MonoBehaviour
{

    public GameObject player;
    SpriteRenderer sr;
   // Rigidbody2D rb;
    Animator animator;
    BoxCollider2D boxCollider;
    public float timeUnderground = 10f;
    public float timeAboveground = 6f;
    public float timer;
    public float timer2;
    public float transistion = 1.5f;
    public bool Aboveground = false;
    public bool flag = false;
    public bool flag2 = false;

    public bool slowed = false;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        timer = timeUnderground;
       // rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
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
                timeAboveground = 10f;
                slowed = false;
            }
        }

        timer -= Time.deltaTime;
        
        if (timer < 0)
        {
            switchState();
        } 

        if (flag)
        {
            if (timer2 < 0)
            {
                sr.enabled = false;
                this.gameObject.layer = 14;
                flag = false;
                boxCollider.enabled = false;
            }
            timer2 -= Time.deltaTime;
        }

        if (flag2)
        {
            if (timer2 < 0)
            {
                this.gameObject.layer = 10;
                flag2 = false;
                boxCollider.enabled = true;
            }
            timer2 -= Time.deltaTime;
        }
    }

    public void switchState()
    {
        if (Aboveground)
        {
            timer = timeUnderground;
            animator.SetTrigger("Dig");
            //animator.SetFloat("Up", 0f);
            timer2 = transistion + 1;
            flag = true;
        } else
        {
            timer = timeAboveground;
            animator.SetTrigger("Dig");
            //animator.SetFloat("Up", 1f);
            this.gameObject.transform.position = player.transform.position;
            flag2 = true;
            timer2 = transistion;
            sr.enabled = true;
        }

        
        Aboveground = !Aboveground;

    }

}
