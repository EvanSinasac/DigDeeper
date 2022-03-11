using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBatWingsQuest : Quest
{
    void Start()
    {
        QuestName = "Art Project";
        QuestType = "CollectBatWingsQuest";
        Description = "Collect 40 Bat Wings";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Bronze Spear");
        //Debug.Log(Reward.name);
        RewardAmount = 1; //5 Spaghet

        Goals.Add(new CollectionGoal(this, "Bat Wings", "Collect 40 Bat Wings", false, 0, 40));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
