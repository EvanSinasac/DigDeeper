using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  Quest Manager keeps track of quests between scenes
 * */

public class QuestManager : MonoBehaviour
{
    //GameObject quests;
    public Quest[] Quests { get; set; }
    public Quest Quest;
    public int index = 0;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("quests");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);


        Quests = this.gameObject.GetComponents<Quest>();

        if (PlayerPrefs.GetInt("sarahHasActiveQuest") == 1)
        {
            index = 0;
            Quest qs = null;

            foreach (Quest q in Quests)
            {
                if (q.name == PlayerPrefs.GetString("sarahQuestName"))
                {
                    qs = q;
                    break;
                } //end of if
            }

            if (qs == null)
            {
                this.gameObject.AddComponent(System.Type.GetType(PlayerPrefs.GetString("sarahQuestType")));
            }

        } //end of if sarah

        if (PlayerPrefs.GetInt("mitchHasActiveQuest") == 1)
        {
            index = 0;
            Quest qs = null;

            foreach (Quest q in Quests)
            {
                if (q.name == PlayerPrefs.GetString("mitchQuestName"))
                {
                    qs = q;
                    break;
                } //end of if
            }

            if (qs == null)
            {
                this.gameObject.AddComponent(System.Type.GetType(PlayerPrefs.GetString("mitchQuestType")));
            }

        } //end of if mitch

        if (PlayerPrefs.GetInt("karlHasActiveQuest") == 1)
        {
            index = 0;
            Quest qs = null;

            foreach (Quest q in Quests)
            {
                if (q.name == PlayerPrefs.GetString("karlQuestName"))
                {
                    qs = q;
                    break;
                } //end of if
            }

            if (qs == null)
            {
                this.gameObject.AddComponent(System.Type.GetType(PlayerPrefs.GetString("karlQuestType")));
            }

        } //end of if karl

        if (PlayerPrefs.GetInt("clancyHasActiveQuest") == 1)
        {
            index = 0;
            Quest qs = null;

            foreach (Quest q in Quests)
            {
                if (q.name == PlayerPrefs.GetString("clancyQuestName"))
                {
                    qs = q;
                    break;
                } //end of if
            }

            if (qs == null)
            {
                this.gameObject.AddComponent(System.Type.GetType(PlayerPrefs.GetString("clancyQuestType")));
            }

        } //end of if mitch

    } //end of Awake

    
} //end of QuestManager
