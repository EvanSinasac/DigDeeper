using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  Quest base class for the different kinds of quests and the definitions needed for a quest with potentially multiple goals
 * Credit to GameGrind and their amazing questing tutorial videos (Specifically: https://www.youtube.com/watch?v=jN-27UawCgU)
 * */
 
public class Quest : MonoBehaviour
{

    public List<QuestGoal> Goals { get; set; } = new List<QuestGoal>();
    public string QuestName { get; set; }
    public string Description { get; set; }
    public Item Reward { get; set; }
    public int RewardAmount { get; set; }
    public bool Completed { get; set; }
    public string QuestType { get; set; }


    public void CheckGoals ()
    {
        Completed = Goals.All(g => g.Completed);
    } //end of CheckGoals   

    public void GiveReward()
    {
        if (Reward != null)
        {
            if (GameManager.instance.inventoryContainer != null)
            {
                GameManager.instance.inventoryContainer.Add(Reward, RewardAmount);
            } //end of if
        } //end of if
    } //end of GiveReward

    public string GetQuestType ()
    {
        return QuestType;
    }

    public string GetQuestName()
    {
        return QuestName;
    }

    public string GetQuestDescription()
    {
        return Description;
    }

    /*public string GetQuestReward ()
    {
        return (RewardAmount.ToString() + " " + Reward.name);
    }*/
    
} //end of Quest
