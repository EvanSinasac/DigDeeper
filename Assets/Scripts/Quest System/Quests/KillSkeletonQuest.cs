using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSkeletonQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Skeleton Smasher";
        QuestType = "KillSkeletonQuest";
        Description = "Kill 4 Skeletons";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Bronze Sword");
        //Debug.Log(Reward.name);
        RewardAmount = 1; //1 Bronze Sword

        Goals.Add(new KillGoal(this, 7, "Kill 4 Skeletons", false, 0, 4));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
