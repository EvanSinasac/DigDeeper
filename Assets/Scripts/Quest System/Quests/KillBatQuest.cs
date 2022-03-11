using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  KillBatQuest is a quest to kill Bats
 * Credit to GameGrind and their amazing questing tutorial videos (Specifically: https://www.youtube.com/watch?v=LONrh-6xbXQ)
 * */

public class KillBatQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Beat Up Bats";
        QuestType = "KillBatQuest";
        Description = "Kill 15 Bats";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Magic Essence");
        //Debug.Log(Reward.name);
        RewardAmount = 10; //10 Magic Essence

        Goals.Add(new KillGoal(this, 2, "Kill 15 Bats", false, 0, 15));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
