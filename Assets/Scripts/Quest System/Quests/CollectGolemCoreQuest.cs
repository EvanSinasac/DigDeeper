using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectGolemCoreQuest : Quest
{
    void Start()
    {
        QuestName = "Oven Repair";
        QuestType = "CollectGolemCoreQuest";
        Description = "Collect 5 Golem Cores";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Coffee");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Coffee

        Goals.Add(new CollectionGoal(this, "Golem Core", "Collect 5 Golem Cores", false, 0, 5));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
