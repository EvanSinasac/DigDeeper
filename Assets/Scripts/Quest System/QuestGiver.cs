using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : TalkInteract
{

    public bool AssignedQuest { get; set; }
    public bool Helped { get; set; }


    public GameObject quests;

    [SerializeField] public string questType;

    public Quest Quest { get; set; }
    public Quest[] Quests { get; set; }

    [SerializeField] public DialogContainer questDialogue;
    [SerializeField] public DialogContainer notDoneDialogue;
    [SerializeField] public DialogContainer completedDialogue;
    [SerializeField] public DialogContainer afterDialogue;

    public bool hasQuestToGive;
    public int index = 0;

    private bool checkedQuest = false;
    

    private void Start()
    {
        quests = GameObject.FindGameObjectWithTag("quests");
       /* PlayerPrefs.SetString("sarahQuestName", "");
        PlayerPrefs.SetString("sarahQuestDescription", "");
        PlayerPrefs.SetString("sarahQuestReward", "");*/
        

        if (PlayerPrefs.GetInt("sarahHasActiveQuest") == 1)
        {
            AssignedQuest = true;
            Helped = false;
            hasQuestToGive = false;
            //Quest = quests.GetComponent<Quest>();
            Quests = quests.GetComponents<Quest>();

            while ((Quest = Quests[index]) != null) {
                if (Quest.QuestType.Contains(questType) && Quest.name == PlayerPrefs.GetString("sarahQuestName"))
                {
                    break;
                } else
                {
                    index++;
                }
            }

        } else
        {
            AssignedQuest = false;
            Helped = false;
            hasQuestToGive = true;
        }

        if (PlayerPrefs.GetInt("sarahHasBeenHelped") == 1)
        {
            Helped = true;
        } //end of if
        else
            Helped = false;
    } //end of start


    void FixedUpdate()
    {

        if (Quest == null)
        {
            foreach (Quest q in quests.GetComponents<Quest>())
            {
                Debug.Log(q.QuestType);
                if (q.QuestType == questType)
                {
                    Quest = q;
                    break;
                } //end of if
            } //end of foreach
            checkedQuest = false;
        } //end of if
        else if (!checkedQuest && AssignedQuest)
        {
            Debug.Log("Quest is not null");
            Debug.Log(Quest.GetQuestName());

            PlayerPrefs.SetString("sarahQuestName", Quest.GetQuestName());
            PlayerPrefs.SetString("sarahQuestDescription", Quest.GetQuestDescription());
            PlayerPrefs.SetString("sarahQuestReward", (string)(Quest.RewardAmount + " " + Quest.Reward.name));

            PlayerPrefs.Save();

            checkedQuest = true;
        } //end of else if

    } //end of FixedUpdate

    public override void Interact(Character character)
    {
        if (hasQuestToGive)
        {
            //base.Interact(character);
            GameManager.instance.dialogSystem.Initialize(questDialogue);
            AssignQuest();
        } else if (AssignedQuest && !Helped){
            CheckQuest();
        } else
        {
            GameManager.instance.dialogSystem.Initialize(afterDialogue);
            //DialogSystem.Instance.AddNewDialog(new string[] {"Thanks for that stuff that one time."}, name);
        }
    }

    void AssignQuest ()
    {
        AssignedQuest = true;
        hasQuestToGive = false;

        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        
        //Debug.Log(Quest.QuestName);

        PlayerPrefs.SetInt("sarahHasActiveQuest", 1);
        PlayerPrefs.SetInt("sarahHasBeenHelped", 0);

        PlayerPrefs.Save();
        
    }

    public void CheckQuest ()
    {
        foreach (QuestGoal g in Quest.Goals)
        {
            g.Evaluate();
        }
        //Quest.CheckGoals();
        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GameManager.instance.dialogSystem.Initialize(completedDialogue);

            
            Destroy(Quest);

            PlayerPrefs.SetInt("sarahHasActiveQuest", 0);
            PlayerPrefs.SetInt("sarahHasBeenHelped", 1);

            PlayerPrefs.SetString("sarahQuestName", "");
            PlayerPrefs.SetString("sarahQuestDescription", "");
            PlayerPrefs.SetString("sarahQuestReward", "");

            PlayerPrefs.Save();

            //DialogSystem.Instance.AddNewDialog(new string[] {"Thanks for that!  Here's your reward.", "More dialog"}, name);
        } else
        {
            GameManager.instance.dialogSystem.Initialize(notDoneDialogue);
            //DialogSystem.Instance.AddNewDialog(new string[] {"You're still in the middle of helping me!  Get back at it!"}, name);
        }
    }
    

}
