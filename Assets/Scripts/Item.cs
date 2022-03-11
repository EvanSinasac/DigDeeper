using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Item")]
[System.Serializable]
public class Item : ScriptableObject
{
    [SerializeField] string id;
    public string ID { get { return id; } }
    public string Name;
    public bool stackable;
    public bool consumable;
    public Sprite icon;
    public ToolAction OnAction;
}
