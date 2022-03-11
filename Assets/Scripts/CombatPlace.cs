using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon/Placeable")]
public class CombatPlace : ToolAction
{
    [SerializeField] GameObject objectToPlace;

    public override bool OnApply(Vector2 worldPoint)
    {
        if (!GameManager.instance.player.GetComponent<PlayerMovement>().shopping)
        {
            worldPoint += GameManager.instance.player.GetComponent<PlayerMovement>().lastMotionVector;

            Instantiate(objectToPlace, worldPoint, Quaternion.identity); //Spawn item infront of player
        }
        return true;
    } //end of OnApply
} //end of CombatPlace
