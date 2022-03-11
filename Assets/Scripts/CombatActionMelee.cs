using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Data/Weapon/Melee")]
public class CombatActionMelee : ToolAction
{
    [SerializeField] float sizeOfInteractableArea;
    [SerializeField] float offsetDistance;
    [SerializeField] int damage;
    [SerializeField] float knockback;
    [SerializeField] float attackSpeed;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    [SerializeField] List<WeaponType> weaponTypes;
    [SerializeField] List<LevelType> levelTypes;
    //Enchantments 
    [SerializeField] bool lifeSteal = false;
    [SerializeField] bool beastSlayer = false;
    [SerializeField] float beastSlayerIncrease;
    [SerializeField] List<EnemyType> beastBuff;
    [SerializeField] bool constructSlayer = false;
    [SerializeField] float constructSlayerIncrease;
    [SerializeField] List<EnemyType> constructBuff;
    [SerializeField] bool humanoidSlayer = false;
    [SerializeField] float humanoidSlayerIncrease;
    [SerializeField] List<EnemyType> humanoidBuff;
    [SerializeField] bool amorphousSlayer = false;
    [SerializeField] float amorphousSlayerIncrease;
    [SerializeField] List<EnemyType> amorphousBuff;
    [SerializeField] bool slow = false;
    [SerializeField] float slowBy = 0.2f;
    //[SerializeField] bool freeze = false;
    [SerializeField] bool poison = false;
    [SerializeField] bool explosion = false;
    [SerializeField] GameObject objectToPlace;

    public override bool OnApply(Vector2 worldPoint)
    {

        GameObject player = GameManager.instance.player;
        if (!player.GetComponent<PlayerMovement>().getSwinging() && !player.GetComponent<PlayerMovement>().shopping)
        {

            player.GetComponent<PlayerMovement>().swing(attackSpeed);

            worldPoint = worldPoint + GameManager.instance.player.GetComponent<PlayerMovement>().lastMotionVector * offsetDistance;
            // Debug.Log("Attack Location: x=" + worldPoint.x + " y=" + worldPoint.y);

            setLifeSteal(PlayerPrefs.GetInt("lifeStealActive") == 1);
            setBeastSlayer(PlayerPrefs.GetInt("beastSlayerActive") == 1);
            setConstructSlayer(PlayerPrefs.GetInt("constructSlayerActive") == 1);
            setHumanoidSlayer(PlayerPrefs.GetInt("humanoidSlayerActive") == 1);
            setAmorphousSlayer(PlayerPrefs.GetInt("amorphousSlayerActive") == 1);
            setSlow(PlayerPrefs.GetInt("slowActive") == 1);
            setPoison(PlayerPrefs.GetInt("poisonActive") == 1);
            setExplosion(PlayerPrefs.GetInt("explosionActive") == 1);


            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

            foreach (Collider2D c in colliders)
            {
                ToolHit hit = c.GetComponent<ToolHit>();
                int damageToBeDealt = damage;

                if (hit != null)
                {
                    Debug.Log("Weapon Hit");
                    if (hit.CanBeHit(canHitNodesOfType) == true)
                    {
                        Vector2 difference = c.transform.position - GameManager.instance.player.transform.position;// Note change to be an actual force
                                                                                                                   // c.transform.position = new Vector2(c.transform.position.x + difference.x, c.transform.position.y + difference.y) * knockback;
                                                                                                                   //c.GetComponent<Rigidbody2D>().AddForceAtPosition(new Vector2(c.transform.position.x + difference.x, c.transform.position.y + difference.y) * knockback, c.transform.position);

                       // Debug.Log("difference.normalized * knockback: " + (difference.normalized * knockback));
                        c.GetComponent<Rigidbody2D>().AddForce(difference.normalized * knockback);
                       // Debug.Log("Enemy Veclocity: " + c.GetComponent<Rigidbody2D>().velocity);


                        if (beastSlayer && hit.CanGetSlayerBuff(beastBuff)) //Beast Slayer Enchant
                        {
                            Debug.Log("Beast Slayer Active");
                            /*  hit.Hit(BeastSlayer(damage));

                              if (lifeSteal) //Life Steal Enchant
                              {
                                  Debug.Log("Life Steal Active");
                                  LifeSteal(BeastSlayer(damage));
                              }*/
                            damageToBeDealt = BeastSlayer(damage);
                        }

                        if (constructSlayer && hit.CanGetSlayerBuff(constructBuff)) //Construct Slayer Enchant
                        {
                            /*  hit.Hit(ConstructSlayer(damage));
                              Debug.Log("Construct Slayer Active - Damage: " + ConstructSlayer(damage));

                              if (lifeSteal) //Life Steal Enchant
                              {
                                  Debug.Log("Life Steal Active");
                                  LifeSteal(ConstructSlayer(damage));
                              }*/
                            damageToBeDealt = ConstructSlayer(damage);
                        }

                        if (humanoidSlayer && hit.CanGetSlayerBuff(humanoidBuff)) //Humanoid Slayer Enchant
                        {
                            /* Debug.Log("Humanoid Slayer Active");
                             hit.Hit(HumanoidSlayer(damage));

                             if (lifeSteal) //Life Steal Enchant
                             {
                                 Debug.Log("Life Steal Active");
                                 LifeSteal(HumanoidSlayer(damage));
                             }*/
                            damageToBeDealt = HumanoidSlayer(damage);
                        }

                        if (amorphousSlayer && hit.CanGetSlayerBuff(amorphousBuff)) //Amorphous Slayer Enchant
                        {
                            /*Debug.Log("Amorphous Slayer Active");
                            hit.Hit(AmorphousSlayer(damage));

                            if (lifeSteal) //Life Steal Enchant
                            {
                                Debug.Log("Life Steal Active");
                                LifeSteal(AmorphousSlayer(damage));
                            }*/
                            damageToBeDealt = AmorphousSlayer(damage);
                        }

                        // if (!(beastSlayer || constructSlayer || humanoidSlayer || amorphousSlayer))
                        //{

                        hit.Hit(damageToBeDealt);


                        if (lifeSteal) //Life Steal Enchant
                        {
                            Debug.Log("Life Steal Active");
                            LifeSteal(damageToBeDealt);
                        }
                        //}

                        if (slow) //Slow Enchant
                        {
                            Slow(c);
                        }

                        if (poison) //Poison Enchant
                        {
                            Poison(c);
                        }

                        if (player != null)
                        {
                            player.GetComponent<Animator>().SetTrigger(weaponTypes[0].ToString());
                            player.GetComponent<Animator>().SetTrigger(levelTypes[0].ToString());
                        }

                        return true;

                    }
                }
            } //end of foreach

            if (explosion) //Explosion Enchant
            {
               // Debug.Log("Explosion Active");
                Explosion(worldPoint);
            }

            if (player != null)
            {
                player.GetComponent<Animator>().SetTrigger(weaponTypes[0].ToString());
                player.GetComponent<Animator>().SetTrigger(levelTypes[0].ToString());
            }

            return false;
        } else
        {
            return false;
        }
    } //end of OnApply

    public void LifeSteal(int damage) 
    {
        //Adds the damage player deals to their health 
        GameManager.instance.player.GetComponent<Character>().ChangeHealth(damage);
    } //end of LifeSteal

    public void Freeze(Collider2D enemy)
    {
        //Lock position and set move speed to 0??
        if (enemy.GetComponent<BatController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<GolemController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<MoleController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<RatController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<SkeletonController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<SlimeController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<SnekController>() != null)
        {
            //do something
        }

        if (enemy.GetComponent<WitchController>() != null)
        {
            //do something
        }

    } //end of Freeze

    public void Slow(Collider2D enemy)
    {
        //Decrease movement speed
        if (enemy.GetComponent<BatController>() != null)
        {
            //slow bat
            enemy.GetComponent<BatController>().slowedTimer = 10f;
            enemy.GetComponent<BatController>().slowed = true;
            float batMoveSpeed = enemy.GetComponent<BatController>().moveSpeed;

            enemy.GetComponent<BatController>().moveSpeed = batMoveSpeed - (batMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<GolemController>() != null)
        {
            //slow golem
            enemy.GetComponent<GolemController>().slowedTimer = 10f;
            enemy.GetComponent<GolemController>().slowed = true;
            float golemMoveSpeed = enemy.GetComponent<GolemController>().speed;

            enemy.GetComponent<GolemController>().speed = golemMoveSpeed - (golemMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<MoleController>() != null)
        {
            //slow mole
            enemy.GetComponent<MoleController>().slowedTimer = 10f;
            enemy.GetComponent<MoleController>().slowed = true;
            float moleAboveGround = enemy.GetComponent<MoleController>().timeAboveground;

            enemy.GetComponent<MoleController>().timeAboveground = moleAboveGround + (moleAboveGround * slowBy);
        }

        if (enemy.GetComponent<RatController>() != null)
        {
            //do something
            enemy.GetComponent<RatController>().slowedTimer = 10f;
            enemy.GetComponent<RatController>().slowed = true;
            float ratMoveSpeed = enemy.GetComponent<RatController>().speed;

            enemy.GetComponent<RatController>().speed = ratMoveSpeed - (ratMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<SkeletonController>() != null)
        {
            //do something
            enemy.GetComponent<SkeletonController>().slowedTimer = 10f;
            enemy.GetComponent<SkeletonController>().slowed = true;
            float skeletonMoveSpeed = enemy.GetComponent<SkeletonController>().moveSpeed;

            enemy.GetComponent<SkeletonController>().moveSpeed = skeletonMoveSpeed - (skeletonMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<SlimeController>() != null)
        {
            //do something
            
            enemy.GetComponent<SlimeController>().slowedTimer = 10f;
            enemy.GetComponent<SlimeController>().slowed = true;
            float slimeMoveSpeed = enemy.GetComponent<SlimeController>().moveSpeed;

            enemy.GetComponent<SlimeController>().moveSpeed = (slimeMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<SnekController>() != null)
        {
            //do something
            enemy.GetComponent<SnekController>().slowedTimer = 10f;
            enemy.GetComponent<SnekController>().slowed = true;
            float snekMoveSpeed = enemy.GetComponent<SnekController>().moveSpeed;

            enemy.GetComponent<SnekController>().moveSpeed = snekMoveSpeed - (snekMoveSpeed * slowBy);
        }

        if (enemy.GetComponent<WitchController>() != null)
        {
            //do something
            enemy.GetComponent<WitchController>().slowedTimer = 10f;
            enemy.GetComponent<WitchController>().slowed = true;
            float witchTeleportSpeed = enemy.GetComponent<WitchController>().timeToTeleport;
            
            enemy.GetComponent<WitchController>().timeToTeleport = witchTeleportSpeed + (witchTeleportSpeed * slowBy);
        } 

    } //end of Slow

    public void Poison(Collider2D enemy)
    {
        //Set public float and timer from function on hit
        enemy.GetComponent<EnemyController>().poisonedTimer = 5f;
        enemy.GetComponent<EnemyController>().poisoned = true;
    } //end of Poison

    public void Explosion(Vector2 worldPoint)
    {
        //Spawn bomb on swing
        Instantiate(objectToPlace, worldPoint, Quaternion.identity); //Spawn item infront of player
    } //end of Explosion

    //Enemy Slayer
    public int BeastSlayer(int damage)
    {
        return damage + (int)(damage * beastSlayerIncrease);
    } //end of BeastSlayer

    public int ConstructSlayer(int damage)
    {
        return damage + (int)(damage * constructSlayerIncrease);
    } //end of ConstructSlayer

    public int HumanoidSlayer(int damage)
    {
        return damage + (int)(damage * humanoidSlayerIncrease);
    } //end of HumanoidSlayer

    public int AmorphousSlayer(int damage)
    {
        return damage + (int)(damage * amorphousSlayerIncrease);
    } //end of AmorphousSlayer


    //public bool getLifeSteal()
    //{
    //    return lifeSteal;
    //} //end of getLifeSteal

    //public bool getBeastSlayer()
    //{
    //    return beastSlayer;
    //} //end of getBeastSlayer

    //public bool getConstructSlayer()
    //{
    //    return constructSlayer;
    //} //end of getConstructSlayer

    //public bool getHumanoidSlayer()
    //{
    //    return humanoidSlayer;
    //} //end of getHumanoidSlayer

    //public bool getAmorphousSlayer()
    //{
    //    return amorphousSlayer;
    //} //end of getAmorphousSlayer

    //public bool getSlow()
    //{
    //    return slow;
    //} //end of getSlow

    //public bool getPoison()
    //{
    //    return poison;
    //} //end of poison

    //public bool getExplosion()
    //{
    //    return explosion;
    //} //end of explosion
    
    public void setLifeSteal(bool x)
    {
        lifeSteal = x;
    } //end of getLifeSteal

    public void setBeastSlayer(bool x)
    {
        beastSlayer = x;
    } //end of getBeastSlayer

    public void setConstructSlayer(bool x)
    {
        constructSlayer = x;
    } //end of getConstructSlayer

    public void setHumanoidSlayer(bool x)
    {
        humanoidSlayer = x;
    } //end of getHumanoidSlayer

    public void setAmorphousSlayer(bool x)
    {
        amorphousSlayer = x;
    } //end of getAmorphousSlayer

    public void setSlow(bool x)
    {
        slow = x;
    } //end of getSlow

    public void setPoison(bool x)
    {
        poison = x;
    } //end of poison

    public void setExplosion(bool x)
    {
        explosion = x;
    } //end of explosion

}
