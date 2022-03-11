using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: April 1, 2021
 * */

public class UIManager : MonoBehaviour
{

    GameObject[] pauseObjects;
    GameObject[] inventoryObjects;
    GameObject[] toolbarObjects;
    GameObject questMenu;

    GameObject sarahPanel;
    GameObject sarahQuestName;
    GameObject sarahQuestDescription;
    GameObject sarahQuestReward;

    GameObject mitchPanel;
    GameObject mitchQuestName;
    GameObject mitchQuestDescription;
    GameObject mitchQuestReward;

    GameObject karlPanel;
    GameObject karlQuestName;
    GameObject karlQuestDescription;
    GameObject karlQuestReward;

    GameObject clancyPanel;
    GameObject clancyQuestName;
    GameObject clancyQuestDescription;
    GameObject clancyQuestReward;

    GameObject amorphousSlayerIcon;
    GameObject beastSlayerIcon;
    GameObject constructSlayerIcon;
    GameObject humanoidSlayerIcon;
    GameObject poisonIcon;
    GameObject slowIcon;
    GameObject lifeStealIcon;
    GameObject explosionIcon;
    GameObject speedIcon;
    GameObject defenseIcon;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        pauseObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        inventoryObjects = GameObject.FindGameObjectsWithTag("InventoryPause");
        toolbarObjects = GameObject.FindGameObjectsWithTag("ToolbarPause");
        questMenu = GameObject.FindGameObjectWithTag("QuestMenu");

        sarahPanel = GameObject.FindGameObjectWithTag("SarahPanel");
        sarahQuestName = GameObject.FindGameObjectWithTag("SarahQuestName");
        sarahQuestDescription = GameObject.FindGameObjectWithTag("SarahQuestDescription");
        sarahQuestReward = GameObject.FindGameObjectWithTag("SarahQuestReward");

        mitchPanel = GameObject.FindGameObjectWithTag("MitchPanel");
        mitchQuestName = GameObject.FindGameObjectWithTag("MitchQuestName");
        mitchQuestDescription = GameObject.FindGameObjectWithTag("MitchQuestDescription");
        mitchQuestReward = GameObject.FindGameObjectWithTag("MitchQuestReward");

        karlPanel = GameObject.FindGameObjectWithTag("KarlPanel");
        karlQuestName = GameObject.FindGameObjectWithTag("KarlQuestName");
        karlQuestDescription = GameObject.FindGameObjectWithTag("KarlQuestDescription");
        karlQuestReward = GameObject.FindGameObjectWithTag("KarlQuestReward");

        clancyPanel = GameObject.FindGameObjectWithTag("ClancyPanel");
        clancyQuestName = GameObject.FindGameObjectWithTag("ClancyQuestName");
        clancyQuestDescription = GameObject.FindGameObjectWithTag("ClancyQuestDescription");
        clancyQuestReward = GameObject.FindGameObjectWithTag("ClancyQuestReward");

        amorphousSlayerIcon = GameObject.FindGameObjectWithTag("AmorphousSlayer");
        beastSlayerIcon = GameObject.FindGameObjectWithTag("BeastSlayer");
        constructSlayerIcon = GameObject.FindGameObjectWithTag("ConstructSlayer");
        humanoidSlayerIcon = GameObject.FindGameObjectWithTag("HumanoidSlayer");
        poisonIcon = GameObject.FindGameObjectWithTag("Poison");
        slowIcon = GameObject.FindGameObjectWithTag("Slow");
        lifeStealIcon = GameObject.FindGameObjectWithTag("LifeSteal");
        explosionIcon = GameObject.FindGameObjectWithTag("ExplosionIcon");
        speedIcon = GameObject.FindGameObjectWithTag("SpeedIcon");
        defenseIcon = GameObject.FindGameObjectWithTag("DefenseIcon");

        hidePaused();
        hideInventory();
        hideQuests();
        hideIcons();
    } //end of start

    // Update is called once per frame
    void Update()
    {

        //uses the Esc key to pause and unpause the game
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showPaused();
            } else if (Time.timeScale == 0)
            {
                Debug.Log("high");
                Time.timeScale = 1;
                hidePaused();
            } //end of else if
        } //end of if (Escape)

        //E to opend and close the inventory
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showInventory();
            }
            else if (Time.timeScale == 0)
            {
               // Debug.Log("high");
                Time.timeScale = 1;
                hideInventory();
            } //end of else if
        } //end of if (E)

        //Q to epen and close the quest menu
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Time.timeScale == 1)
            {
                Time.timeScale = 0;
                showQuests();

            } else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                hideQuests();
            } //end of else if
        } //end of if (Q)

        if (PlayerPrefs.GetFloat("speedPowerUp") > 1) {
            showIcon(speedIcon);
        } else
        {
            hideIcon(speedIcon);
        } //end of else

        if (PlayerPrefs.GetFloat("defencePowerUp") > 1)
        {
            showIcon(defenseIcon);
        } else
        {
            hideIcon(defenseIcon);
        } //end of else

        if (PlayerPrefs.GetInt("lifeStealActive") == 1)
        {
            showIcon(lifeStealIcon);
        } else
        {
            hideIcon(lifeStealIcon);
        } //end of else

        if (PlayerPrefs.GetInt("beastSlayerActive") == 1)
        {
            showIcon(beastSlayerIcon);
        } else
        {
            hideIcon(beastSlayerIcon);
        } //end of else

        if (PlayerPrefs.GetInt("constructSlayerActive") == 1)
        {
            showIcon(constructSlayerIcon);
        } else {
            hideIcon(constructSlayerIcon);
        } //end of else
        
        if (PlayerPrefs.GetInt("humanoidSlayerActive") == 1)
        {
            showIcon(humanoidSlayerIcon);
        } else
        {
            hideIcon(humanoidSlayerIcon);
        } //end of else

        if (PlayerPrefs.GetInt("amorphousSlayerActive") == 1)
        {
            showIcon(amorphousSlayerIcon);
        } else
        {
            hideIcon(amorphousSlayerIcon); 
        } //end of else
            
        if (PlayerPrefs.GetInt("slowActive") == 1)
        {
            showIcon(slowIcon);
        } else
        {
            hideIcon(slowIcon);
        } //end of else

        if (PlayerPrefs.GetInt("poisonActive") == 1)
        {
            showIcon(poisonIcon);
        } else
        {
            hideIcon(poisonIcon);
        } //end of else

        if (PlayerPrefs.GetInt("explosionActive") == 1)
        {
            showIcon(explosionIcon);
        } else
        {
            hideIcon(explosionIcon);
        } //end of else

    } //end of update

    //Reloads the Level
    public void Reload()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    } //end of reload

    //controls the pausing of the scene
    public void pauseControl()
    {
        if(Time.timeScale == 1)
        {
            Time.timeScale = 0;
            showPaused();
        } else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            hidePaused();
        } //end of else if
    } //end of pauseControl

    //Show objects with ShowOnPaused tag
    public void showPaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(true);
        } //end of foreach
    } //end of showPaused

    //hides objects with ShowOnPaused tag
    public void hidePaused()
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(false);
        } //end of foreach
    } //end of hidePaused

    //loads inputted level
    public void LoadLevel(string level)
    {
        //Application.LoadLevel(level);
        SceneManager.LoadScene(level);
    } //end of LoadLevel

    public void showInventory()
    {
        foreach (GameObject g in inventoryObjects)
        {
            g.SetActive(true);
        } //end of foreach
        
        foreach (GameObject g in toolbarObjects)
        {
            g.SetActive(false);
        } //end of foreach
    } //end of showInventory

    public void hideInventory()
    {
        foreach (GameObject g in inventoryObjects)
        {
            g.SetActive(false);
        } //end of foreach

        foreach (GameObject g in toolbarObjects)
        {
            g.SetActive(true);
        } //end of foreach
    } //end of hideInventory

    //Exit Mine
    public void exitMine()
    {
        Debug.Log("Current Level on Exit: "+ PlayerPrefs.GetInt("currentLevel"));
        PlayerPrefs.SetInt("exitLevel", PlayerPrefs.GetInt("currentLevel"));

        PlayerPrefs.SetInt("lifeStealActive", 0);
        PlayerPrefs.SetInt("beastSlayerActive",0);
        PlayerPrefs.SetInt("constructSlayerActive", 0);
        PlayerPrefs.SetInt("humanoidSlayerActive", 0);
        PlayerPrefs.SetInt("amorphousSlayerActive", 0);
        PlayerPrefs.SetInt("slowActive", 0);
        PlayerPrefs.SetInt("poisonActive", 0);
        PlayerPrefs.SetInt("explosionActive", 0);

        PlayerPrefs.Save();
        SceneController.prevScene = "LevelGenV2";
        LoadLevel("Town Scene");
    } //end of exitMine

    public void showQuests()
    {
        questMenu.SetActive(true);
        if(PlayerPrefs.GetInt("sarahHasActiveQuest") == 1)
        {
            sarahPanel.SetActive(true);
            sarahQuestName.GetComponent<Text>().text = PlayerPrefs.GetString("sarahQuestName");
            sarahQuestDescription.GetComponent<Text>().text = PlayerPrefs.GetString("sarahQuestDescription");
            sarahQuestReward.GetComponent<Text>().text = PlayerPrefs.GetString("sarahQuestReward");
        } else
        {
            sarahPanel.SetActive(false);
        } //end of else

        if (PlayerPrefs.GetInt("mitchHasActiveQuest") == 1)
        {
            mitchPanel.SetActive(true);
            mitchQuestName.GetComponent<Text>().text = PlayerPrefs.GetString("mitchQuestName");
            mitchQuestDescription.GetComponent<Text>().text = PlayerPrefs.GetString("mitchQuestDescription");
            mitchQuestReward.GetComponent<Text>().text = PlayerPrefs.GetString("mitchQuestReward");
        } else
        {
            mitchPanel.SetActive(false);
        } //end of else

        if (PlayerPrefs.GetInt("karlHasActiveQuest") == 1)
        {
            karlPanel.SetActive(true);
            karlQuestName.GetComponent<Text>().text = PlayerPrefs.GetString("karlQuestName");
            karlQuestDescription.GetComponent<Text>().text = PlayerPrefs.GetString("karlQuestDescription");
            karlQuestReward.GetComponent<Text>().text = PlayerPrefs.GetString("karlQuestReward");
        }
        else
        {
            karlPanel.SetActive(false);
        } //end of else

        if (PlayerPrefs.GetInt("clancyHasActiveQuest") == 1)
        {
            clancyPanel.SetActive(true);
            clancyQuestName.GetComponent<Text>().text = PlayerPrefs.GetString("clancyQuestName");
            clancyQuestDescription.GetComponent<Text>().text = PlayerPrefs.GetString("clancyQuestDescription");
            clancyQuestReward.GetComponent<Text>().text = PlayerPrefs.GetString("clancyQuestReward");
        }
        else
        {
            clancyPanel.SetActive(false);
        } //end of else

    } //end of showQuests

    public void hideQuests()
    {
        questMenu.SetActive(false);
        if (PlayerPrefs.GetInt("sarahHasActiveQuest") == 1)
        {
            sarahPanel.SetActive(false);
        } //end of if

        if (PlayerPrefs.GetInt("mitchHasActiveQuest") == 1)
        {
            mitchPanel.SetActive(false);
        } //end of if

        if (PlayerPrefs.GetInt("karlHasActiveQuest") == 1)
        {
            karlPanel.SetActive(false);
        } //end of if

        if (PlayerPrefs.GetInt("clancyHasActiveQuest") == 1)
        {
            clancyPanel.SetActive(false);
        } //end of if
    } //end of hideQuests

    public void hideIcons()
    {
        amorphousSlayerIcon.SetActive(false);
        beastSlayerIcon.SetActive(false);
        constructSlayerIcon.SetActive(false);
        humanoidSlayerIcon.SetActive(false);
        poisonIcon.SetActive(false);
        slowIcon.SetActive(false);
        lifeStealIcon.SetActive(false);
        explosionIcon.SetActive(false);
        speedIcon.SetActive(false);
        defenseIcon.SetActive(false);
    } //end of hideIcons

    public void hideIcon (GameObject icon)
    {
        icon.SetActive(false);
    } //end of hideIcon

    public void showIcon(GameObject icon)
    {
        icon.SetActive(true);
    } //end of showIcon

} //end of UIManager
