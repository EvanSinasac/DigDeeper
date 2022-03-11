using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClancysUltimateQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "The Ultimate Art Project";
        QuestType = "ClanysUltimateQuest";
        Description = "Collect 10 each of Bat Wings, Slime, Bones and Rat Tails.";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Bronze Pickax");
        //Debug.Log(Reward.name);
        RewardAmount = 1; //5 Spaghet

        Goals.Add(new CollectionGoal(this, "Bat Wings", "Collect 10 Bat Wings", false, 0, 10));
        Goals.Add(new CollectionGoal(this, "Slime", "Collect 10 Slime", false, 0, 10));
        Goals.Add(new CollectionGoal(this, "Bones", "Collect 10 Bones", false, 0, 10));
        Goals.Add(new CollectionGoal(this, "Rat Tails", "Collect 10 Rat Tails", false, 0, 10));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
