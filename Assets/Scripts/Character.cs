using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*
 * Evan Sinasac - 104571345
 * Last Worked On: January 21, 2021
 * */

public class Character : MonoBehaviour
{
    //Health Control
    public int maxHealth = 100;
    int currenthealth;

    public int health { get { return currenthealth; } }

    public HealthBar healthBar;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;

    public float powerUpTimer = 0;
    bool powerUp = false;

    Animator animator;

    public Quest quest;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GameObject.FindObjectOfType<HealthBar>();
        currenthealth = PlayerPrefs.GetInt("currentHealth");//maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(currenthealth);
        animator = GetComponent<Animator>();

         //PlayerPrefs.SetInt("newGame", 0);
       // PlayerPrefs.SetInt("lifeStealActive", 0);
        //Can't have these when we build the game but required for testing
        /*   PlayerPrefs.SetInt("powerUp", 0);
           PlayerPrefs.SetFloat("powerUpTimer", 0);
           PlayerPrefs.SetFloat("defencePowerUp", 1);*/
       /*   PlayerPrefs.SetInt("sarahHasActiveQuest", 0);
          PlayerPrefs.SetInt("sarahHasBeenHelped", 0);
          PlayerPrefs.SetInt("sarahAssignedQuest", 0); //quick reset
          PlayerPrefs.SetInt("mitchHasActiveQuest", 0);
          PlayerPrefs.SetInt("mitchHasBeenHelpred", 0);
          PlayerPrefs.SetInt("mitchAssignedQuest", 0);
          PlayerPrefs.SetInt("karlHasActiveQuest", 0);
          PlayerPrefs.SetInt("karlHasBeenHelped", 0);
          PlayerPrefs.SetInt("karlAssignedQuest", 0);
          PlayerPrefs.SetInt("clancyHasActiveQuest", 0);
          PlayerPrefs.SetInt("clancyHasBeenHelped", 0);
          PlayerPrefs.SetInt("clancyAssignedQuest", 0);
        PlayerPrefs.SetString("sarahQuestType", "");
        PlayerPrefs.SetString("karlQuestType", "");
        PlayerPrefs.SetString("mitchQuestType", "");
        PlayerPrefs.SetString("clancyQuestType", "");
        
        PlayerPrefs.SetInt("lifeStealActive", 0);
        PlayerPrefs.SetInt("beastSlayerActive", 0);
        PlayerPrefs.SetInt("constructSlayerActive", 0);
        PlayerPrefs.SetInt("humanoidSlayerActive", 0);
        PlayerPrefs.SetInt("amorphousSlayerActive", 0);
        PlayerPrefs.SetInt("slowActive", 0);
        PlayerPrefs.SetInt("poisonActive", 0);
        PlayerPrefs.SetInt("explosionActive", 0);*/
    }

    // Update is called once per frame
    void Update()
    {
        //Testing
       /* if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeHealth(-20);
        }*/

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                this.gameObject.layer = 9; //player
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Exit Level: "+PlayerPrefs.GetInt("exitLevel"));
        }

        if (currenthealth <= 0) {
            GameOver();
        }

        if (PlayerPrefs.GetInt("powerUp") == 1)
        {
            powerUp = true;
            powerUpTimer = PlayerPrefs.GetFloat("powerUpTimer");
            PlayerPrefs.Save();
        }
        

        if (powerUp)
        {
            if (powerUpTimer <= 0)
            {
                powerUp = false;
                PlayerPrefs.SetFloat("speedPowerUp", 1);
                PlayerPrefs.SetFloat("defencePowerUp", 1);
                PlayerPrefs.SetInt("powerUp", 0);

                PlayerPrefs.Save();
            }
            else
            {
                powerUpTimer -= Time.deltaTime;

                PlayerPrefs.SetFloat("powerUpTimer", powerUpTimer);
                PlayerPrefs.Save();
            }

        }


    }

    private void GameOver()
    {
        PlayerPrefs.SetInt("currentHealth", 100);

        for (int i = 0; i < PlayerPrefs.GetInt("currentLevel"); i++)
        {
            GameManager.instance.inventoryContainer.RemoveRandom();
        }

        PlayerPrefs.SetInt("currentLevel", 1);

        //GameManager.instance.GetComponent<UIManager>().exitMine();
        PlayerPrefs.SetInt("lifeStealActive", 0);
        PlayerPrefs.SetInt("beastSlayerActive", 0);
        PlayerPrefs.SetInt("constructSlayerActive", 0);
        PlayerPrefs.SetInt("humanoidSlayerActive", 0);
        PlayerPrefs.SetInt("amorphousSlayerActive", 0);
        PlayerPrefs.SetInt("slowActive", 0);
        PlayerPrefs.SetInt("poisonActive", 0);
        PlayerPrefs.SetInt("explosionActive", 0);

        PlayerPrefs.Save();
        SceneController.prevScene = "LevelGenV2";
        SceneManager.LoadScene("Town Scene");

        //SceneManager.LoadScene(1);
    }

    private void FixedUpdate()
    {
        if (currenthealth != PlayerPrefs.GetInt("currentHealth"))
        {
            currenthealth = Mathf.Clamp(PlayerPrefs.GetInt("currentHealth"), 0, PlayerPrefs.GetInt("maxHealth")); //from what I understand, Mathf.Clamp is basically the the combination of the if statement I wrote before
            healthBar.SetHealth(currenthealth);
            PlayerPrefs.SetInt("currentHealth", currenthealth);
        }
    }

    /*   // Function to remove health from player when they take damage 
       void TakeDamage(int damage)
       {
           currenthealth -= damage;
           healthBar.SetHealth(currenthealth);
       }*/

    //replacement for TakeDamage that will take negative numbers for damaging effects and positive numbers for healing effects
    public void ChangeHealth (int amount)
    {
        if (amount < 0)
        {

            if (isInvincible)
                return;

            animator.SetTrigger("Hit");
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            this.gameObject.layer = 12; //player ghost

            currenthealth = Mathf.Clamp(currenthealth + (int)(amount / PlayerPrefs.GetFloat("defencePowerUp")), 0, PlayerPrefs.GetInt("maxHealth")); //from what I understand, Mathf.Clamp is basically the the combination of the if statement I wrote before
            
        } else
            currenthealth = Mathf.Clamp(currenthealth + amount, 0, PlayerPrefs.GetInt("maxHealth")); //from what I understand, Mathf.Clamp is basically the the combination of the if statement I wrote before


        //more efficient?
        healthBar.SetHealth(currenthealth);
        PlayerPrefs.SetInt("currentHealth", currenthealth);

        //Debug.Log("Player Perf(maxHealth):" + PlayerPrefs.GetInt("maxHealth"));
        Debug.Log("Player Perf(currentHealth): " + PlayerPrefs.GetInt("currentHealth"));
    } //end of ChangeHealth


    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            ChangeHealth(-20);
        }
    }


}
