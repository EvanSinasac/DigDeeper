using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*(I'm sure I'll have to do something in here eventually lol)
 * Evan Sinasac - 104571345 && Matthew Pizzo - 
 * Last Worked On: March 29, 2021 - added itemDatabase to reference quest rewards
 * */

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public ItemSaveManager itemSaveManager;
    public ItemContainer inventoryContainer;
    public ItemContainer storageContainer;
    public ItemDatabase itemDatabase;
    // [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        itemSaveManager.LoadInventory(inventoryContainer);
        itemSaveManager.LoadStorage(storageContainer);
        //healthBar.SetSize(0.4f);
        //PlayerPrefs.SetInt("maxHealth", 100);
    }

    private void OnDestroy()
    {
        itemSaveManager.SaveInventory(inventoryContainer);
        itemSaveManager.SaveStorage(storageContainer);
    }

    private void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public GameObject player;
    public Text floorLevel;
    public ItemDragAndDropController dragAndDropController;
    public DialogSystem dialogSystem;

    public void SaveInventory()
    {
        itemSaveManager.SaveInventory(inventoryContainer);
    }

    public void SaveStorage()
    {
        itemSaveManager.SaveStorage(storageContainer);
    }
}
