using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }

    public List<Quest> AvailableQuests = new();
    public List<Quest> HoldingQuests = new();
    public List<Quest> ActiveQuests = new();

    [SerializeField] QuestWindow window;
    [SerializeField] QuestDetailWindow detailWindow;

    readonly QuestType[] allTypes = (QuestType[])Enum.GetValues(typeof(QuestType));
    readonly Rank[] allRanks = (Rank[])Enum.GetValues(typeof(Rank));

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        for (var i = 0; i < 5; i++)
        {
            var randomQuest = RandomQuest();
            if (!AvailableQuests.Contains(randomQuest))
            {
                AvailableQuests.Add(randomQuest);
            }
        }
    }

    #region Window

    public void ShowWindow()
    {
        window.ShowWindow();
    }

    public void HideWindow()
    {
        window.HideWindow();
    }

    public void ShowDetailWindow(Quest quest)
    {
        detailWindow.Show(quest);
    }

    public void HideDetailWindow(Quest quest)
    {
        detailWindow.Hide();
    }

    #endregion

    public Quest RandomQuest()
    {
        Quest randomQuest;
        int tries = 0;
        while (true)
        {
            if (TryGetRandomQuest(out randomQuest)) break;
            if (tries++ == 10)
            {
                // Fallback
                return GuildManager.Instance.QuestConfig.QuestPool[0];
            }
        }
        return randomQuest;
    }

    bool TryGetRandomQuest(out Quest result)
    {
        result = null;

        QuestType randomType = default;
        Rank randomRank = default;

        var config = GuildManager.Instance.QuestConfig;
        var randomValue = Random.value;
        for (var i = 0; i < allTypes.Length; i++)
        {
            if (randomValue < config.TypeCdf[i])
            {
                randomType = (QuestType)i;
                break;
            }
        }

        randomValue = Random.value;
        for (var i = 0; i < allRanks.Length; i++)
        {
            if (randomValue < config.RankCdf[i])
            {
                randomRank = (Rank)i;
                break;
            }
        }

        List<Quest> hit = new();
        foreach (var quest in config.QuestPool)
        {
            if (quest.Type == randomType && quest.Rank == randomRank) hit.Add(quest);
        }

        if (hit.Count == 0) return false;
        result = hit[Random.Range(0, hit.Count)];
        return true;
    }

    public void TakeQuestFromBoard(Quest quest)
    {
        AvailableQuests.Remove(quest);
        HoldingQuests.Add(quest);

        window.RemoveQuest(quest);
    }
}
