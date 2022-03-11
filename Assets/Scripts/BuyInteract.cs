using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyInteract : Interactable
{
	public Button yourButton;
	[SerializeField] Item itemNeeded;
	[SerializeField] int amountNeeded;
	[SerializeField] Item itemPurchased;
	[SerializeField] GameObject shopPanel;
	[SerializeField] Text itemAmount;
	[SerializeField] Image itemImage;

	public ItemContainer inventoryContainer;

	public override void Interact(Character character)
	{
		shopPanel.SetActive(true);
	}

	void Start()
	{
		inventoryContainer = GameManager.instance.inventoryContainer;
		yourButton = GetComponent<Button>();
		yourButton.onClick.AddListener(TaskOnClick);

		if (itemNeeded != null)
		{
			itemAmount.text = "Need:\n- " + amountNeeded + " " + itemNeeded.Name;
		}
		else 
		{
			itemAmount.text = "Need:\n- " + amountNeeded;
		}

		itemImage.sprite = itemPurchased.icon;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				//Debug.LogWarning("Clicking outside the menu");
				shopPanel.SetActive(false);
                //Time.timeScale = 1;
                GameManager.instance.player.GetComponent<PlayerMovement>().Shopping(false);
			}
		}
	}

	void TaskOnClick()
	{
		if (itemNeeded != null) {
			ItemSlot itemSlot = inventoryContainer.slots.Find(x => x.item == itemNeeded);
			if (itemSlot == null) { return; }
			Debug.Log("Item found");

			if (itemSlot.count < amountNeeded) { return; }
			Debug.Log("Item amount enough");

			inventoryContainer.Remove(itemSlot.item, amountNeeded); //Remove materials
		}

		inventoryContainer.Add(itemPurchased); //Add purchased item
	}
}
