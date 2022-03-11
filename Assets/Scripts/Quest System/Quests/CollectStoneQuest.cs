using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectStoneQuest : Quest
{
    void Start()
    {
        QuestName = "Straight Trade";
        QuestType = "CollectStoneQuest";
        Description = "Collect 30 Stone";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Iron");
        //Debug.Log(Reward.name);
        RewardAmount = 30; //30 iron

        Goals.Add(new CollectionGoal(this, "Stone", "Collect 30 Stone", false, 0, 30));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
