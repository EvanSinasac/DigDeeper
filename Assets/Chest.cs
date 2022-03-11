using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Chest : Interactable
{
    [SerializeField] GameObject storagePanel;
    // Start is called before the first frame update

    public override void Interact(Character character)
    {
        //Time.timeScale = 0;
        storagePanel.SetActive(true);
        GameManager.instance.player.GetComponent<PlayerMovement>().Shopping(true);
    }


}
