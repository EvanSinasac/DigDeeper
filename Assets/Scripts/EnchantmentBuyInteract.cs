using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EnchantmentType
{
	lifeSteal,
	beastSlayer,
	constructSlayer,
	humanoidSlayer,
	amorphousSlayer,
	slow,
	poison,
	explosion
}

public class EnchantmentBuyInteract : MonoBehaviour
{
	public Button enchantButton;
	[SerializeField] Item itemNeeded;
	[SerializeField] int amountNeeded;
	[SerializeField] Sprite enchantmentImage;
	[SerializeField] GameObject enchantPanel;
	[SerializeField] Text itemAmount;
	[SerializeField] Image enchantShopIcon;

	[SerializeField] List<EnchantmentType> enchantType;

	public ItemContainer inventoryContainer;

	// Start is called before the first frame update
	void Start()
    {
		inventoryContainer = GameManager.instance.inventoryContainer;
		enchantButton = this.GetComponent<Button>();
		enchantButton.onClick.AddListener(TaskOnClick);
		itemAmount.text = "Need:\n- " + amountNeeded + " " + itemNeeded.Name;
		enchantShopIcon.sprite = enchantmentImage;
	}

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(1))
		{
			if (EventSystem.current.IsPointerOverGameObject() == false)
			{
				//Debug.LogWarning("Clicking outside the menu");
				enchantPanel.SetActive(false);
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

		inventoryContainer.Remove(itemSlot.item, amountNeeded); //Remove purchase materials 

        switch (enchantType[0].ToString())
        {
			case "lifeSteal":
				Debug.Log("Enchant 0");
				PlayerPrefs.SetInt("lifeStealActive", 1);
				break;

			case "beastSlayer":
				Debug.Log("Enchant 1");
				PlayerPrefs.SetInt("beastSlayerActive", 1);
				break;

			case "constructSlayer":
				Debug.Log("Enchant 2");
				PlayerPrefs.SetInt("constructSlayerActive", 1);
				break;

			case "humanoidSlayer":
				Debug.Log("Enchant 3");
				PlayerPrefs.SetInt("humanoidSlayerActive", 1);
				break;

			case "amorphousSlayer":
				Debug.Log("Enchant 4");
				PlayerPrefs.SetInt("amorphousSlayerActive", 1);
				break;

			case "slow":
				Debug.Log("Enchant 5");
				PlayerPrefs.SetInt("slowActive", 1);
				break;

			case "poison":
				Debug.Log("Enchant 6");
				PlayerPrefs.SetInt("poisonActive", 1);
				break;

			case "explosion":
				Debug.Log("Enchant 7");
				PlayerPrefs.SetInt("explosionActive", 1);
				break;

			default:
				break;
        }

    }
}
