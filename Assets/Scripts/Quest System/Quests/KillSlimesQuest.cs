using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  KillSlimesQuest is a quest to kill slimes
 * Credit to GameGrind and their amazing questing tutorial videos (Specifically: https://www.youtube.com/watch?v=LONrh-6xbXQ)
 * */

public class KillSlimesQuest : Quest
{
    // Start is called before the first frame update
    void Start()
    {
        QuestName = "Slay the Slimes";
        QuestType = "KillSlimesQuest";
        Description = "Kill 10 Slimes";
        Reward = GameManager.instance.itemDatabase.GetItemReferenceName("Iron");
        //Debug.Log(Reward.name);
        RewardAmount = 30; //30 Iron Ore

        Goals.Add(new KillGoal(this, 0, "Kill 10 Slimes", false, 0, 10));
        //Goals.Add(new CollectionGoal(this, "Slime", "Collect 20 slime", false, 0, 20));

        Goals.ForEach(g => g.Init());
    }
    
}
