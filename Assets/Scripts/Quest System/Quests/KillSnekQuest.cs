using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSnekQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Sneks Slitherings";
        QuestType = "KillSnekQuest";
        Description = "Kill 7 Sneks";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Rat Tails");
        //Debug.Log(Reward.name);
        RewardAmount = 10; //30 Iron Ore

        Goals.Add(new KillGoal(this, 5, "Kill 7 Sneks", false, 0, 7));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
