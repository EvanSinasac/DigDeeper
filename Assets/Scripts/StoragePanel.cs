using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class StoragePanel : ItemPanel
{
    public override void OnClick(int id)
    {
        GameManager.instance.dragAndDropController.OnClick(inventory.slots[id]);
        Show();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && this.transform.parent.gameObject)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {
                GameManager.instance.itemSaveManager.SaveStorage(GameManager.instance.storageContainer);
                //Debug.LogWarning("Clicking outside the menu");
                this.transform.parent.gameObject.SetActive(false);
                //Time.timeScale = 1;
                GameManager.instance.player.GetComponent<PlayerMovement>().Shopping(false);
            }
        }
    }
}
