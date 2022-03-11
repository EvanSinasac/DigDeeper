using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionGoal : QuestGoal
{

    public string ItemName { get; set; }

    public ItemContainer inventoryContainer;
    public Item requiredItem;

    public CollectionGoal(Quest quest, string itemID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.ItemName = itemID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;

       /* inventoryContainer = GameManager.instance.inventoryContainer;
        requiredItem = GameManager.instance.itemDatabase.GetItemReferenceName(ItemName);
        ItemSlot itemSlot = inventoryContainer.slots.Find(x => x.item == requiredItem);
        this.CurrentAmount = itemSlot.count;*/

    } //end of constructor CollectionGoal

    public override void Init()
    {
        base.Init();
        InventoryEventHandler.OnItemAddedToInventory += ItemPickedUp;
    } //end of Init

    void ItemPickedUp(Item item)
    {//don't really need this anymore but keeping it in case we revert back to it
        if (item.name == this.ItemName)
        {
            Debug.Log("Detected item pickup: " + this.ItemName);
            //this.CurrentAmount++;
            //Evaluate();
        } //end of if 
    } //end of EnemyDied

    public override void Evaluate()
    {

       // if (!Completed)
      //  {
            //Debug.Log("Collection Evaluate");
            inventoryContainer = GameManager.instance.inventoryContainer;

            if (inventoryContainer != null)
            {
                //Debug.Log("container good");
                requiredItem = GameManager.instance.itemDatabase.GetItemReferenceName(ItemName);

                if (requiredItem != null)
                {
                    //Debug.Log("requiredItem good");
                    ItemSlot itemSlot = inventoryContainer.slots.Find(x => x.item == requiredItem);

                    if (itemSlot != null)
                    {
                        //Debug.Log("itemSlot good");
                        this.CurrentAmount = itemSlot.count;

                        if (CurrentAmount >= RequiredAmount)
                        {
                            inventoryContainer.Remove(itemSlot.item, RequiredAmount);
                            Complete();
                        } //end of if
                    } //end of if
                } //end of if

            } //end of if
       // } //end of if
    } //end of evaluate
    
} //end of Collection Goal
