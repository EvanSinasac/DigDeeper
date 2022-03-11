using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRatTailsQuest : Quest
{
    void Start()
    {
        QuestName = "Please Don't Ask";
        QuestType = "CollectRatTailsQuest";
        Description = "Collect 10 Rat Tails";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Spaghet");
        //Debug.Log(Reward.name);
        RewardAmount = 10; //10 Spaghet

        Goals.Add(new CollectionGoal(this, "Rat Tails", "Collect 10 Rat Tails", false, 0, 10));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
