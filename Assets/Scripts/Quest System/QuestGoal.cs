using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * Author: Evan Sinasac - 104571345
 * Date Last Worked On: March 28, 2021
 * Description:  QuestGoal base class for the different kinds of goals and what's needed for a QuestGoal to be defined.
 * Credit to GameGrind and their amazing questing tutorial videos (Specifically: https://www.youtube.com/watch?v=up6HcYph_bo)
 * */
 
public class QuestGoal
{

    public Quest Quest { get; set; }
    public string Description { get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequiredAmount { get; set; }


    public virtual void Init ()
    {
        //default init stuff
    } //end of Init

    public virtual void Evaluate()
    {
        if (CurrentAmount >= RequiredAmount)
        {
            Complete();
        } //end of if

        //return Completed;
    } //end of Evaluate

    public void Complete()
    {

        Completed = true;
        Quest.CheckGoals();
    } //end of Complete

   
} //end of QuestGoal


