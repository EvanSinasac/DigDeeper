using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceNodeType { 
    Undefined,
    Enemy,
    Stone,
    Ore
}

public enum WeaponType
{
    Undefined,
    Sword,
    Spear,
    Hammer,
    Pickaxe
}

public enum LevelType
{
    Undefined,
    Basic,
    Copper
}

[CreateAssetMenu(menuName ="Data/Tool")]
public class GatherResourceNode : ToolAction
{
    [SerializeField] float sizeOfInteractableArea = 1f;
    [SerializeField] float offsetDistance = 1f;
    [SerializeField] int damage;
    [SerializeField] float attackSpeed;
    [SerializeField] List<ResourceNodeType> canHitNodesOfType;
    [SerializeField] List<WeaponType> weaponTypes;
    [SerializeField] List<LevelType> levelTypes;

    public override bool OnApply(Vector2 worldPoint)
    {
        GameObject player = GameManager.instance.player;

        if (!player.GetComponent<PlayerMovement>().getSwinging() && !player.GetComponent<PlayerMovement>().shopping)
        {
            worldPoint = worldPoint * offsetDistance;
            Collider2D[] colliders = Physics2D.OverlapCircleAll(worldPoint, sizeOfInteractableArea);

            foreach (Collider2D c in colliders)
            {
                ToolHit hit = c.GetComponent<ToolHit>();
                if (hit != null)
                {
                    if (hit.CanBeHit(canHitNodesOfType) == true)
                    {
                        hit.Hit(damage);

                        if (player != null)
                        {
                            player.GetComponent<PlayerMovement>().swing(attackSpeed);
                            player.GetComponent<Animator>().SetTrigger(weaponTypes[0].ToString());
                            player.GetComponent<Animator>().SetTrigger(levelTypes[0].ToString());
                        }

                        return true;
                    }
                }
            }


            if (player != null)
            {
                player.GetComponent<PlayerMovement>().swing(attackSpeed);
                player.GetComponent<Animator>().SetTrigger(weaponTypes[0].ToString());
                player.GetComponent<Animator>().SetTrigger(levelTypes[0].ToString());
            }

            return false;
        } else
        {
            return false;
        }

    } //end of OnApply
}
