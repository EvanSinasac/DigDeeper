using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "EmptyQuest";
        QuestType = "EmptyQuest";
        Description = "Empty quest for protection against null index";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Stone");
        //Debug.Log(Reward.name);
        RewardAmount = 1; //5 Spaghet

        Goals.Add(new EmptyGoal(this, Description, false, 0, 1));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }

}
