using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  KillGoal is for quests to kill a certain number of enemies.
 * Credit to GameGrind and their amazing questing tutorial videos (Specifically: https://www.youtube.com/watch?v=h7rRic4Xoak)
 * */

public class KillGoal : QuestGoal
{

    public int EnemyID { get; set; }
    

    public KillGoal (Quest quest, int enemyID, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.EnemyID = enemyID;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
    } //end of constructor KillGoal

    public override void Init()
    {
        base.Init();
        CombatEvents.OnEnemyDeath += EnemyDied;

    } //end of Init

    void EnemyDied (EnemyController enemy)
    {
        if (enemy.ID == this.EnemyID)
        {
            Debug.Log("Detected enemy death: " + this.EnemyID);
            this.CurrentAmount++;
            Evaluate();
        } //end of if 
    } //end of EnemyDied

} //end of KillGoal
