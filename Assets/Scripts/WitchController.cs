using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 23, 2021
 * WitchController for the Witch enemy movement behaviour
 * Witches will teleport near the player and launch a spell at them
 * */


public class WitchController : MonoBehaviour
{

    public GameObject spellPrefab;
    Rigidbody2D rb;
    public GameObject player;
    GameObject spellObject;
    Animator animator;
    Vector2 lookDirection;

    public float timeToTeleport = 10f;
    public float timeToAttack = 3f;
    public float timer;

    public bool teleportFlag = true;
    public bool attackFlag = false;
    public bool spawn = false;
    public bool waitFlag = false;

    public float distanceAway = 6f;

    public int direction = 0;

    public bool slowed = false;
    public float slowedBy;
    public float slowedTimer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        timer = timeToTeleport;
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
                timeToTeleport = 10f;
                slowed = false;
            }
        }

        if (teleportFlag)
        {
            if (timer < 0)
            {
                teleportFlag = false;
                waitFlag = true;
                Teleport();
                timer = 1f;
            } //end of if
        } //end of if

        if (waitFlag)
        {
            if (timer < 0)
            {
                attackFlag = true;
                spawn = true;
                waitFlag = false;
                timer = timeToAttack;
            } //end of if
        } //end of if

        if (attackFlag)
        {
            if (spawn)
            {
                spellObject = Instantiate(spellPrefab, rb.position + Vector2.up * 0.75f, Quaternion.identity);
                animator.SetTrigger("Attack");
                spawn = false;
            } //end of if

            if (timer < 0)
            {
                teleportFlag = true;
                attackFlag = false;
                Attack(spellObject);
                timer = timeToTeleport;
            } //end of if
        } //end of if

        timer -= Time.deltaTime;

    } //end of Update

    private void FixedUpdate()
    {
        //lookDirection = player.transform.position - transform.position;
        lookDirection = player.GetComponent<Rigidbody2D>().position - (rb.position + Vector2.up * 0.75f);

        lookDirection.Normalize();
        animator.SetFloat("Dir X", lookDirection.x);
        animator.SetFloat("Dir Y", lookDirection.y);
    } //end of FixedUpdate

    void Attack (GameObject spellObject)
    {
        WitchSpell spell = spellObject.GetComponent<WitchSpell>();
        spell.Launch(lookDirection, 300);
    } //end of attack

    void Teleport ()
    {
        direction = Random.Range(0, 4);
        switch (direction)
        {
            case 0: //left
                this.transform.position = new Vector2(player.transform.position.x - distanceAway, player.transform.position.y);
                break;
            case 1: //right
                this.transform.position = new Vector2(player.transform.position.x + distanceAway, player.transform.position.y);
                break;
            case 2: //up
                this.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + distanceAway);
                break;
            case 3: //down
                this.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - distanceAway);
                break;
            default:
                break;
        } //end of switch
    } //end of teleport

    /*void attack ()
    {

    }*/
}
