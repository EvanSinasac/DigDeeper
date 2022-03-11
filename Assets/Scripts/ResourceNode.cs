using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
/*
 * Matthew Pizzo - 
 * Last Worked On: January 17, 2021
 * */

public class ResourceNode : ToolHit
{
    [SerializeField] GameObject pickUpDrop;
    [SerializeField] float spread = 0.7f;
    [SerializeField] int health = 10;
    [SerializeField] int dropCount = 5;
    [SerializeField] int itemCountOneDrop = 1;
    [SerializeField] Item item;
    [SerializeField] ResourceNodeType nodeType;

    public override void Hit(int damage) {
        if (health <= 0)
        {
            while (dropCount > 0)
            {
                dropCount -= 1;

                Vector3 position = transform.position;
                position.x += spread * UnityEngine.Random.value - spread / 2;
                position.y += spread * UnityEngine.Random.value - spread / 2;

                ItemSpawnManager.instance.SpawnItem(position, item, itemCountOneDrop);
            }

            Destroy(gameObject);
        }
        else {
            health = health - damage;
        }

        Debug.Log("Node Health: "+health);
    }

    public override bool CanBeHit(List<ResourceNodeType> canBeHit)
    {
        return canBeHit.Contains(nodeType);
    }

    private void OnParticleCollision(GameObject other)
    {
        Hit(20);
    }
}
