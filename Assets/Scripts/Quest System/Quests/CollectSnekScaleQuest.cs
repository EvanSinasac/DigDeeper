using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectSnekScaleQuest : Quest
{
    void Start()
    {
        QuestName = "New China";
        QuestType = "CollectSnekScaleQuest";
        Description = "Collect 8 Snek Scales";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Bronze Sword");
        //Debug.Log(Reward.name);
        RewardAmount = 1; //1 Bronze Sword

        Goals.Add(new CollectionGoal(this, "Snek Scale", "Collect 8 Snek Scales", false, 0, 8));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
