using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KarlMultiQuestGiver : QuestGiver
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
        index = 0;
        setQuest = 0;

        if (PlayerPrefs.GetInt("karlHasActiveQuest") == 1)
        {
            AssignedQuest = true;
            Helped = false;
            PlayerPrefs.SetInt("karlHasBeenHelped", 0);
            hasQuestToGive = false;

            Quests = quests.GetComponents<Quest>();

            foreach (Quest q in Quests)
            {
                if (q.QuestType == questType)
                {
                    Quest = q;
                }
            }

            switch (PlayerPrefs.GetInt("karlAssignedQuest"))
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
            AssignedQuest = false;
            Helped = false;

            setQuest = Random.Range(0, 20);
            if (setQuest <= 5)
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

        if (PlayerPrefs.GetInt("karlHasBeenHelped") == 1)
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
        else if (!haveCheckedQuest && AssignedQuest && PlayerPrefs.GetString("karlQuestName") == "")
        {
            Debug.Log("Quest is not null");
            Debug.Log(Quest.GetQuestName());

            PlayerPrefs.SetString("karlQuestName", Quest.GetQuestName());
            PlayerPrefs.SetString("karlQuestDescription", Quest.GetQuestDescription());
            PlayerPrefs.SetString("karlQuestReward", (string)(Quest.RewardAmount + " " + Quest.Reward.name));

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
        PlayerPrefs.SetInt("karlHasActiveQuest", 1);
        PlayerPrefs.SetInt("karlHasBeenHelped", 0);
        PlayerPrefs.SetInt("karlAssignedQuest", setQuest);
        PlayerPrefs.SetString("karlQuestType", questType);

        PlayerPrefs.SetString("karlQuestName", Quest.GetQuestName());
        PlayerPrefs.SetString("karlQuestDescription", Quest.GetQuestDescription());
        PlayerPrefs.SetString("karlQuestReward", (string)(Quest.RewardAmount + " " + Quest.Reward.name));

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

            PlayerPrefs.SetInt("karlHasActiveQuest", 0);
            PlayerPrefs.SetInt("karlHasBeenHelped", 1);
            PlayerPrefs.SetInt("karlAssignedQuest", 0);

            PlayerPrefs.SetString("karlQuestName", "");
            PlayerPrefs.SetString("karlQuestDescription", "");
            PlayerPrefs.SetString("karlQuestReward", "");

            PlayerPrefs.Save();
            //DialogSystem.Instance.AddNewDialog(new string[] {"Thanks for that!  Here's your reward.", "More dialog"}, name);
        } //end of if
        else
        {
            GameManager.instance.dialogSystem.Initialize(notDoneDialogue);
            //DialogSystem.Instance.AddNewDialog(new string[] {"You're still in the middle of helping me!  Get back at it!"}, name);
        } //end of else
    } //end of CheckQuest

} //end of KarlMultiQuestGiver
