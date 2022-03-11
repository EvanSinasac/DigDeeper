using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UpgradeInteract : Interactable
{
	public Button yourButton;
	[SerializeField] Item itemNeeded;
	[SerializeField] int amountNeeded;
	[SerializeField] Item upgradedItem;
	[SerializeField] Item oldItem;
	[SerializeField] GameObject upgradePanel;
	[SerializeField] Text itemAmount;
	[SerializeField] Image upgradeImage;

	public ItemContainer inventoryContainer;

	void Start()
	{
		inventoryContainer = GameManager.instance.inventoryContainer;
		yourButton = this.GetComponent<Button>();
		yourButton.onClick.AddListener(TaskOnClick);
		itemAmount.text = "Need:\n- " + amountNeeded + " " + itemNeeded.Name;
		upgradeImage.sprite = upgradedItem.icon;
	}

	void Update() {
		if (Input.GetMouseButtonDown(1))
		{
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				//Debug.LogWarning("Clicking outside the menu");
				upgradePanel.SetActive(false);
                //Time.timeScale = 1;
                GameManager.instance.player.GetComponent<PlayerMovement>().Shopping(false);
            }
		}
	}

	void TaskOnClick()
	{
		ItemSlot itemSlot = inventoryContainer.slots.Find(x => x.item == itemNeeded);
		if (itemSlot == null) { return; }
		Debug.Log("Item found");

		if (itemSlot.count < amountNeeded) { return; }
		Debug.Log("Item amount enough");

		ItemSlot itemSlotOldItem = inventoryContainer.slots.Find(x => x.item == oldItem);
		if (itemSlotOldItem == null) { return; }

		inventoryContainer.Remove(itemSlotOldItem.item); //Remove old item
		inventoryContainer.Remove(itemSlot.item, amountNeeded); //Remove upgrade materials

		inventoryContainer.Add(upgradedItem); //Add upgraded item
	}
}
