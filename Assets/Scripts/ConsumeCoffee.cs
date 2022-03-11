using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Evan Sinasac - 104571345
 * Last Worked On: March 16, 2021
 * Coffee gives the player a speed boost for 1 minute
 * */
[CreateAssetMenu(menuName = "Data/Consumables/Coffee")]
public class ConsumeCoffee : ToolAction
{
    public override bool OnApply(Vector2 worldPoint)
    {
        if (!GameManager.instance.player.GetComponent<PlayerMovement>().shopping)
        {
            PlayerPrefs.SetInt("powerUp", 1);
            PlayerPrefs.SetFloat("powerUpTimer", 30f);
            PlayerPrefs.SetFloat("speedPowerUp", 2f);
            PlayerPrefs.SetFloat("defencePowerUp", 1f);

            PlayerPrefs.Save();
        }

        return true;
    }
}
