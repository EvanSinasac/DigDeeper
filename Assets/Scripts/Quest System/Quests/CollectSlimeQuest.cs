using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSlimeQuest : Quest
{
    void Start()
    {
        QuestName = "Slime Makeover";
        QuestType = "CollectSlimeQuest";
        Description = "Collect 30 Slime";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Spaghet");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Spaghet

        Goals.Add(new CollectionGoal(this, "Slime", "Collect 30 Slime", false, 0, 30));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
