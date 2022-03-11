using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyGoal : QuestGoal
{

    public EmptyGoal(Quest quest, string description, bool completed, int currentAmount, int requiredAmount)
    {
        this.Quest = quest;
        this.Description = description;
        this.Completed = completed;
        this.CurrentAmount = currentAmount;
        this.RequiredAmount = requiredAmount;
        
    } //end of constructor CollectionGoal

}
