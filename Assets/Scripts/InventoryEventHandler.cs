using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryEventHandler : MonoBehaviour
{

    public delegate void ItemEventHandler(Item item);
    public static event ItemEventHandler OnItemAddedToInventory;
    

    public static void ItemAddedToInventory(Item item)
    {
        if (OnItemAddedToInventory != null)
        {
            OnItemAddedToInventory(item);
        } //end of if
    } //end of ItemAddedToInventory

    public static void ItemAddedToInventory(List<Item> items)
    {
        if (OnItemAddedToInventory != null)
        {
            foreach(Item item in items)
            {
                OnItemAddedToInventory(item);
            } //end of foreach
        } //end of if
    } //end of ItemAddedToInventory

} //end of InventoryEventHandler
