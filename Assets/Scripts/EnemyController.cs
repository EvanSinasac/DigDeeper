using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345, Matthew Pizzo - 
 * Last Worked On: March 29, 2021
 * Used for enemy health and other potentially related elements that will be on every enemy
 * */
public enum EnemyType
{
    beast,
    construct,
    humanoid,
    amorphous
}
public class EnemyController : ToolHit
{

    public int health;
    [SerializeField] ResourceNodeType objectType;
    [SerializeField] EnemyType enemyType;
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;
    [SerializeField] int dropCount = 2;
    [SerializeField] int itemCountOneDrop = 1;
    [SerializeField] Item item;

    public int damage; //damage needs to be negative 

    bool skelly = false;
    public bool skellyDown = false;
    Animator animator;

    public bool poisoned = false;
    public int poisonDamage = 5;
    public float poisonedTimer;

    bool isInvincible = false;
    float timeInvincible = 1f;
    float invincibleTimer;

    public int ID;

    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.name.Contains("Slime"))
        {
            health = 15;
            damage = -5;
            ID = 0;
        } else if (gameObject.name.Contains("Rat"))
        {
            health = 30;
            damage = -10;
            ID = 1;
        } else if (gameObject.name.Contains("Bat"))
        {
            health = 20;
            damage = -10;
            ID = 2;
        } else if (gameObject.name.Contains("Mole"))
        {
            health = 50;
            damage = -15;
            ID = 3;
        } else if (gameObject.name.Contains("Golem"))
        {
            health = 100;
            damage = -5;
            ID = 4;
        } else if (gameObject.name.Contains("Snek"))
        {
            health = 40;
            damage -= 10;
            ID = 5;
        } else if (gameObject.name.Contains("Witch"))
        {
            health = 25;
            damage = -5;
            ID = 6;
        } else if (gameObject.name.Contains("Spell"))
        {
            health = 500;
            damage = -20;
        } else if (gameObject.name.Contains("Skeleton"))
        {
            health = 30;
            damage = -20;
            skelly = true;
            ID = 7;
        } else if (gameObject.name.Contains("Boss"))
        {
            health = 250;
            damage = -10;
            ID = 8;
        }

        animator = GetComponent<Animator>();

        if (PlayerPrefs.GetInt("currentLevel") >= 10)
        {
            damage -= (5 * (int)(PlayerPrefs.GetInt("currentLevel") / 10));
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
            {
                isInvincible = false;
                this.gameObject.layer = 10; //enemy
                if (this.name.Contains("Bat"))
                {
                    this.gameObject.layer = 13; //bat
                }
            }
        }
        //For poison enchantment
        if (poisoned)
        {
            poisonedTimer -= Time.deltaTime;
            Hit(poisonDamage);
            if (poisonedTimer < 0)
            {
                poisoned = false;
            }
        }

    }

    public override void Hit(int damage)
    {

        if (isInvincible)
            return;

        animator.SetTrigger("Hit");
        health = health - damage;

        if (health <= 0 && !skelly)
        {
            /* while (dropCount > 0)
             {
                 dropCount -= 1;

                 Vector3 position = transform.position;
                 position.x += spread * UnityEngine.Random.value - spread / 2;
                 position.y += spread * UnityEngine.Random.value - spread / 2;

                 ItemSpawnManager.instance.SpawnItem(position, item, itemCountOneDrop);
             }

             Destroy(gameObject);*/
            Killed();
        } else if (health <= 0 && skelly){
            animator.SetTrigger("Collapse");
            skellyDown = true;
        }

        isInvincible = true;
        invincibleTimer = timeInvincible;
        this.gameObject.layer = 17; //enemyGhost
        if (this.name.Contains("Bat"))
        {
            this.gameObject.layer = 13; //bat
        }

        Debug.Log("Enemy Health: " + health);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(objectType);
    }

    public override bool CanGetSlayerBuff(List<EnemyType> canGetSlayerBuff)
    {
        return canGetSlayerBuff.Contains(enemyType);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Character player = collision.gameObject.GetComponent<Character>();

        if (player != null)
        {
            player.ChangeHealth(damage);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        Character player = collision.gameObject.GetComponent<Character>();

        if (player != null)
        {
            player.ChangeHealth(damage);
            
        }
    }

    public float getHealth ()
    {
        return health;
    }

    public void setHealth (int newHealth)
    {
        health = newHealth;
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "Explosion")
        {
            Debug.Log("Skelly + Explosion");
            if (skelly && skellyDown)
            {
                Killed();
            } else
            {
                Hit(20);
            }
        }
    }

    /**
     * Killed function that spawns enemy item drops and destroys the enemy
     * */
    private void Killed()
    {
        while (dropCount > 0)
        {
            dropCount -= 1;

            Vector3 position = transform.position;
            position.x += spread * UnityEngine.Random.value - spread / 2;
            position.y += spread * UnityEngine.Random.value - spread / 2;

            ItemSpawnManager.instance.SpawnItem(position, item, itemCountOneDrop);
        }

        CombatEvents.EnemyDied(this); //send an event that this enemy died.  used mostly for quest tracking

        Destroy(gameObject);
    } //end of Killed
}
