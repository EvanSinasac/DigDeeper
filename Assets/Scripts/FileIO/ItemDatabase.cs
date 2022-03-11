using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu]
public class ItemDatabase : ScriptableObject
{
    
    [SerializeField] Item[] items;

    public Item GetItemReference(string itemID) 
    {
        foreach (Item item in items)
        {
            //Debug.Log(item.ID);
            if (item.ID == itemID) 
            {
                //Debug.Log(item.name);
                return item;
            }
        }
        return null;
    }

    public Item GetItemReferenceName (string itemName)
    {
        foreach (Item item in items)
        {
            if (item.name == itemName)
            {
                return item;
            }
        }
        return null;
    }

    #if UNITY_EDITOR
    private void OnValidate()
    {
        LoadItems();
    }

    private void OnEnable()
    {
        EditorApplication.projectChanged += LoadItems;
    }

    private void OnDisable()
    {
        EditorApplication.projectChanged += LoadItems;
    }

    private void LoadItems()
    {
        items = FindAssetsByType<Item>("Assets/Inventory/Items");
    }

    public static T[] FindAssetsByType<T>(params string[] folders) where T : Object
    {
        string type = typeof(T).ToString().Replace("UnityEngine.", "");

        string[] guids;
        if (folders == null || folders.Length == 0)
        {
            guids = AssetDatabase.FindAssets("t:" + type);
        }
        else {
            guids = AssetDatabase.FindAssets("t:" + type, folders);
        }


        T[] assets = new T[guids.Length];

        for (int i = 0; i < guids.Length; i++)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            assets[i] = AssetDatabase.LoadAssetAtPath<T>(assetPath);
        }

        return assets;
    }
    #endif
}

