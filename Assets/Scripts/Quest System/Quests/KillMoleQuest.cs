using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMoleQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Mole Massacre";
        QuestType = "KillMoleQuest";
        Description = "Kill 8 Moles";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Tea");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Tea

        Goals.Add(new KillGoal(this, 3, "Kill 8 Moles", false, 0, 8));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
