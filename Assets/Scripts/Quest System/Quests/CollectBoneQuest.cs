using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectBoneQuest : Quest
{
    void Start()
    {
        QuestName = "Macabre Wall Decor";
        QuestType = "CollectBoneQuest";
        Description = "Collect 10 Bones";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Tea");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Tea

        Goals.Add(new CollectionGoal(this, "Bone", "Collect 10 Bones", false, 0, 10));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
