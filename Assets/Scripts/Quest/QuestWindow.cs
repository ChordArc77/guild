using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestWindow : GuildUIWindow
{
    [SerializeField] GameObject questPrefab;
    [SerializeField] Transform[] questSpawnPos;

    readonly Dictionary<Quest, GameObject> questsOnBoard = new();

    bool populated;

    public override void ShowWindow()
    {
        base.ShowWindow();
        BackStack.Push(HideWindow);

        PopulateQuests();
    }

    void PopulateQuests()
    {
        if (populated) return;
        populated = true;
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
