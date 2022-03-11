using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Evan Sinasac - 104571345
 * Last Worked On: March 16, 2021
 * Tea gies the player a defence boost for a minute
 * */
[CreateAssetMenu(menuName = "Data/Consumables/Tea")]

public class ConsumeTea : ToolAction
{
    public override bool OnApply(Vector2 worldPoint)
    {
        if (!GameManager.instance.player.GetComponent<PlayerMovement>().shopping)
        {
            PlayerPrefs.SetInt("powerUp", 1);
            PlayerPrefs.SetFloat("powerUpTimer", 60);
            PlayerPrefs.SetFloat("speedPowerUp", 1);
            PlayerPrefs.SetFloat("defencePowerUp", 2);

            PlayerPrefs.Save();
        }
    

        return true;
    }
}
