using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Evan Sinasac - 104571345
 * Last Worked On: March 16, 2021
 * Spaghet heals the player for 30 health
 * */
[CreateAssetMenu(menuName = "Data/Consumables/Spaghet")]
public class ConsumeSpaghet : ToolAction
{
    //[SerializeField] GameObject objectToConsume;
    public override bool OnApply(Vector2 worldPoint)
    {
        GameObject player = GameManager.instance.player;
        //Instantiate(objectToConsume, worldPoint, Quaternion.identity); //Spawn item infront of player
        //PlayerPrefs.SetInt("currentHealth", (PlayerPrefs.GetInt("currentHealth") + 30));
        if (player != null && !player.GetComponent<PlayerMovement>().shopping)
        {
            player.GetComponent<Character>().ChangeHealth(30);
        }

        return true;
    }
}
