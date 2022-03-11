using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Matthew Pizzo - 
 * Last Worked On: January 17, 2021
 * */

public class PickUpItem : MonoBehaviour
{
    //Transform player;
    [SerializeField] float speed = 5f; //Speed of object towards player
    [SerializeField] float pickUpDistance = 1.5f;
    [SerializeField] float timeToLive = 10f;
    //public GameManager gameManager = GameManager.instance;
    public GameObject player;
    public Item item;
    public int count = 1;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Set(Item item, int count) {
        this.item = item;
        this.count = count;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = item.icon;
    }

    public void Update() {

        timeToLive -= Time.deltaTime;
        if (timeToLive < 0) {
            Destroy(gameObject);
        }

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > pickUpDistance) {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (distance < 0.1f) {
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(item, count);
                InventoryEventHandler.ItemAddedToInventory(item);
                //Debug.Log("Picked up: " + item.name);
            }
            else {
                Debug.LogWarning("No inventory attached to game controller");
            }

            Destroy(gameObject);
        }
    }
}
