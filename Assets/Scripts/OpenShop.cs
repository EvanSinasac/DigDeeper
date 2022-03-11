using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShop : Interactable
{
    [SerializeField] GameObject upgradePanel;
    public override void Interact(Character character)
    {
        GameManager.instance.player.GetComponent<PlayerMovement>().Shopping(true);
        //Time.timeScale = 0;
        upgradePanel.SetActive(true);
    }
}
