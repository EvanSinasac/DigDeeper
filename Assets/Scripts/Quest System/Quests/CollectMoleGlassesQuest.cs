using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMoleGlassesQuest : Quest
{
    void Start()
    {
        QuestName = "Glass Repair";
        QuestType = "CollectMoleGlassesQuest";
        Description = "Collect 20 pairs of Mole Glasses";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Magic Essence");
        //Debug.Log(Reward.name);
        RewardAmount = 15; //15 Magic Essence

        Goals.Add(new CollectionGoal(this, "Mole Glasses", "Collect 20 pairs of Mole Glasses", false, 0, 20));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
