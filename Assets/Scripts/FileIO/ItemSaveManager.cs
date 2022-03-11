using System.Collections.Generic;
using UnityEngine;

public class ItemSaveManager : MonoBehaviour
{
    [SerializeField] ItemDatabase itemDatabase;

    private const string InventoryFileName = "Inventory";
    private const string StorageFileName = "Storage";

    //Inventory Save/Load
    public void LoadInventory(ItemContainer inventoryPlayer)
    {
        ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(InventoryFileName);
        if (savedSlots == null) { return; }

        inventoryPlayer.ClearAll();

        for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = inventoryPlayer.slots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if (savedSlot == null)
            {
                itemSlot.item = null;
                itemSlot.count = 0;
            }
            else 
            {
                itemSlot.item = itemDatabase.GetItemReference(savedSlot.ItemID);
                itemSlot.count = savedSlot.Amount;
            }
        }
    }

    public void SaveInventory(ItemContainer inventoryPlayer) {
        SaveItems(inventoryPlayer.slots, InventoryFileName);
    }

    //Storage Save/Load
    public void LoadStorage(ItemContainer storagePlayer)
    {
        ItemContainerSaveData savedSlots = ItemSaveIO.LoadItems(StorageFileName);
        if (savedSlots == null) { return; }

        storagePlayer.ClearAll();

        for (int i = 0; i < savedSlots.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = storagePlayer.slots[i];
            ItemSlotSaveData savedSlot = savedSlots.SavedSlots[i];

            if (savedSlot == null)
            {
                itemSlot.item = null;
                itemSlot.count = 0;
            }
            else
            {
                itemSlot.item = itemDatabase.GetItemReference(savedSlot.ItemID);
                itemSlot.count = savedSlot.Amount;
            }
        }
    }

    public void SaveStorage(ItemContainer storagePlayer)
    {
        SaveItems(storagePlayer.slots, StorageFileName);
    }

    private void SaveItems(IList<ItemSlot> itemSlots, string fileName) 
    {
        var saveData = new ItemContainerSaveData(itemSlots.Count);

        for (int i = 0; i < saveData.SavedSlots.Length; i++)
        {
            ItemSlot itemSlot = itemSlots[i];

            if (itemSlot.item == null) {
                saveData.SavedSlots[i] = null;
            }
            else {
                saveData.SavedSlots[i] = new ItemSlotSaveData(itemSlot.item.ID, itemSlot.count);
            }

            ItemSaveIO.SaveItems(saveData, fileName);
        }
    }
}
