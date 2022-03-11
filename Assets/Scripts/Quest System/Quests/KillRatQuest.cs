using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillRatQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Rat Ruiner";
        QuestType = "KillRatQuest";
        Description = "Kill 6 Rats";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Spaghet");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Spaghet

        Goals.Add(new KillGoal(this, 1, "Kill 6 Rats", false, 0, 6));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
