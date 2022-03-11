using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillWitchQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Witch Wacking";
        QuestType = "KillWitchQuest";
        Description = "Kill 1 Witche";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Mole Glasses");
        //Debug.Log(Reward.name);
        RewardAmount = 5; //5 Mole Glasses

        Goals.Add(new KillGoal(this, 6, "Kill 1 Witches", false, 0, 1));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
