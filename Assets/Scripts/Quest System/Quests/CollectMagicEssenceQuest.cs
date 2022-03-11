using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectMagicEssenceQuest : Quest
{
    void Start()
    {
        QuestName = "Potion Experimenting";
        QuestType = "CollectMagicEssenceQuest";
        Description = "Collect 10 Magic Essence";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Snek Scale");
        //Debug.Log(Reward.name);
        RewardAmount = 15; //15 Snek Scale

        Goals.Add(new CollectionGoal(this, "Magic Essence", "Collect 10 Magic Essence", false, 0, 10));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
}
