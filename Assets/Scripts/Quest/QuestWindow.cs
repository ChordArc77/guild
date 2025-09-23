using System.Collections.Generic;
using UnityEngine;

public class QuestWindow : GuildUIWindow
{
    [SerializeField] GameObject questPrefab;
    [SerializeField] Transform[] questSpawnPos;

    readonly Dictionary<Quest, GameObject> questsOnBoard = new();

    void Start()
    {
        PopulateQuests();
    }

    public override void ShowWindow()
    {
        base.ShowWindow();
        BackStack.Push(HideWindow);
    }

    void PopulateQuests()
    {
        for (var i = 0; i < QuestManager.Instance.AvailableQuests.Count; i++)
        {
            var instance = Instantiate(questPrefab, questSpawnPos[i]);
            var quest = instance.GetComponent<QuestButton>().Quest = QuestManager.Instance.AvailableQuests[i];

            questsOnBoard.Add(quest, instance);
        }
    }

    public void RemoveQuest(Quest quest)
    {
        Destroy(questsOnBoard[quest]);
        questsOnBoard.Remove(quest);
    }
}
