using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SarahMultiQuestGiver : QuestGiver
{
    
    [SerializeField] private string questTypeOne;
    [SerializeField] private string questTypeTwo;
    [SerializeField] private string questTypeThree;
    [SerializeField] private string questTypeFour;
    [SerializeField] private string questTypeFive;


    [SerializeField] DialogContainer questDialogueOne;
    [SerializeField] DialogContainer questDialogueTwo;
    [SerializeField] DialogContainer questDialogueThree;
    [SerializeField] DialogContainer questDialogueFour;
    [SerializeField] DialogContainer questDialogueFive;

    [SerializeField] DialogContainer completedDialogueOne;
    [SerializeField] DialogContainer completedDialogueTwo;
    [SerializeField] DialogContainer completedDialogueThree;
    [SerializeField] DialogContainer completedDialogueFour;
    [SerializeField] DialogContainer completedDialogueFive;

    public Quest QuestOne { get; set; }
    public Quest QuestTwo { get; set; }
    public Quest QuestThree { get; set; }
    public Quest QuestFour { get; set; }
    public Quest QuestFive { get; set; }

    int setQuest;

    private bool haveCheckedQuest;

    // Start is called before the first frame update
    void Start()
    {
        quests = GameObject.FindGameObjectWithTag("quests");
        setQuest = 0;

        if (PlayerPrefs.GetInt("sarahHasActiveQuest") == 1)
        {
            AssignedQuest = true;
            Helped = false;
            PlayerPrefs.SetInt("sarahHasBeenHelped", 0);
            hasQuestToGive = false;

            Quests = quests.GetComponents<Quest>();

            foreach (Quest q in Quests)
            {
                if (q.QuestType == questType)
                {
                    Quest = q;
                }
            }

            switch (PlayerPrefs.GetInt("sarahAssignedQuest"))
            {
                case 1:
                    Quest = QuestOne;
                    questDialogue = questDialogueOne;
                    completedDialogue = completedDialogueOne;
                    questType = questTypeOne;
                    break;
                case 2: Quest = 
                        QuestTwo;
                    questDialogue = questDialogueTwo;
                    completedDialogue = completedDialogueTwo;
                    questType = questTypeTwo;
                    break;
                case 3:
                    Quest = QuestThree;
                    questDialogue = questDialogueThree;
                    completedDialogue = completedDialogueThree;
                    questType = questTypeThree;
                    break;
                case 4:
                    Quest = QuestFour;
                    questDialogue = questDialogueFour;
                    completedDialogue = completedDialogueFour;
                    questType = questTypeFour;
                    break;
                case 5:
                    Quest = QuestFive;
                    questDialogue = questDialogueFive;
                    completedDialogue = completedDialogueFive;
                    questType = questTypeFive;
                    break;
                default:
                    break;
            } //end of switch
            
        } //end of if
        else
        {
            AssignedQuest = false;
            Helped = false;

            setQuest = Random.Range(0, 20);
            if (setQuest <= 13)
            {
                setQuest = Random.Range(1, 5);
                hasQuestToGive = true;

                switch (setQuest)
                {
                    case 1:
                        Quest = QuestOne;
                        questDialogue = questDialogueOne;
                        completedDialogue = completedDialogueOne;
                        questType = questTypeOne;
                        break;
                    case 2:
                        Quest = QuestTwo;
                        questDialogue = questDialogueTwo;
                        completedDialogue = completedDialogueTwo;
                        questType = questTypeTwo;
                        break;
                    case 3:
                        Quest = QuestThree;
                        questDialogue = questDialogueThree;
                        completedDialogue = completedDialogueThree;
                        questType = questTypeThree;
                        break;
                    case 4:
                        Quest = QuestFour;
                        questDialogue = questDialogueFour;
                        completedDialogue = completedDialogueFour;
                        questType = questTypeFour;
                        break;
                    case 5:
                        Quest = QuestFive;
                        questDialogue = questDialogueFive;
                        completedDialogue = completedDialogueFive;
                        questType = questTypeFive;
                        break;
                    default:
                        break;
                } //end of switch

            } //end of if
            else
            {
                hasQuestToGive = false;
            }

        } //end of else

        if (PlayerPrefs.GetInt("sarahHasBeenHelped") == 1)
        {
            Helped = true;
        } //end of if
        else
            Helped = false;
        
    } //end of start

    void FixedUpdate()
    {

        if (Quest == null && AssignedQuest)
        {
            foreach (Quest q in quests.GetComponents<Quest>())
            {
                Debug.Log(q.QuestType);
                if (q.QuestType == questType)
                {
                    Quest = q;
                    break;
                }
            }
            haveCheckedQuest = false;
        }
        else if (!haveCheckedQuest && AssignedQuest && PlayerPrefs.GetString("sarahQuestName") == "")
        {
           // Debug.Log("Quest is not null");
            //Debug.Log(Quest.GetQuestName());

            PlayerPrefs.SetString("sarahQuestName", Quest.GetQuestName());
            PlayerPrefs.SetString("sarahQuestDescription", Quest.GetQuestDescription());
            PlayerPrefs.SetString("sarahQuestReward", (string)(Quest.RewardAmount + " " + Quest.Reward.name));

            PlayerPrefs.Save();

            haveCheckedQuest = true;
        } //end of else if

    } //end of FixedUpdate

    public override void Interact(Character character)
    {
        if (hasQuestToGive)
        {
            GameManager.instance.dialogSystem.Initialize(questDialogue);
            AssignQuest();
        } //end of if
        else if (AssignedQuest && !Helped)
        {
            this.CheckQuest();
        } //end of else if
        else
        {
            GameManager.instance.dialogSystem.Initialize(afterDialogue);
        } //end of else
                
    } //end of Interact

    void AssignQuest()
    {
        AssignedQuest = true;
        hasQuestToGive = false;

        Quest = (Quest)quests.AddComponent(System.Type.GetType(questType));
        PlayerPrefs.SetInt("sarahHasActiveQuest", 1);
        PlayerPrefs.SetInt("sarahHasBeenHelped", 0);
        PlayerPrefs.SetInt("sarahAssignedQuest", setQuest);
        PlayerPrefs.SetString("sarahQuestType", questType);

        PlayerPrefs.SetString("sarahQuestName", Quest.GetQuestName());
        PlayerPrefs.SetString("sarahQuestDescription", Quest.GetQuestDescription());
        PlayerPrefs.SetString("sarahQuestReward", (string)(Quest.RewardAmount + " " + Quest.Reward.name));
        
        PlayerPrefs.Save();
    } //end of AssignQuest

    new public void CheckQuest()
    {
        foreach (QuestGoal g in Quest.Goals)
        {
            g.Evaluate();
        }

        if (Quest.Completed)
        {
            Quest.GiveReward();
            Helped = true;
            AssignedQuest = false;
            GameManager.instance.dialogSystem.Initialize(completedDialogue);

            Destroy(Quest);

            PlayerPrefs.SetInt("sarahHasActiveQuest", 0);
            PlayerPrefs.SetInt("sarahHasBeenHelped", 1);
            PlayerPrefs.SetInt("sarahAssignedQuest", 0);

            PlayerPrefs.SetString("sarahQuestName", "");
            PlayerPrefs.SetString("sarahQuestDescription", "");
            PlayerPrefs.SetString("sarahQuestReward", "");

            PlayerPrefs.Save();
            
            //DialogSystem.Instance.AddNewDialog(new string[] {"Thanks for that!  Here's your reward.", "More dialog"}, name);
        } //end of if
        else
        {
            GameManager.instance.dialogSystem.Initialize(notDoneDialogue);
            //DialogSystem.Instance.AddNewDialog(new string[] {"You're still in the middle of helping me!  Get back at it!"}, name);
        } //end of else
    } //end of CheckQuest
    
} //end of SarahMultiQuestGiver
