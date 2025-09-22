using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance { get; private set; }
    [SerializeField] GameObject questPrefab;
    [SerializeField] QuestWindow window;

    readonly QuestType[] allTypes = (QuestType[])Enum.GetValues(typeof(QuestType));
    readonly Rank[] allRanks = (Rank[])Enum.GetValues(typeof(Rank));

    void Awake()
    {
        Instance = this;
    }

    public void ShowWindow()
    {
        window.ShowWindow();
    }

    public void HideWindow()
    {
        window.HideWindow();
    }

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

        var config = GuildManager.Instance.QuestConfig;
        var randomValue = Random.value;
        QuestType randomType = default;
        Rank randomRank = default;
        for (var i = 0; i < allTypes.Length; i++)
        {
            if (randomValue < config.TypeCdf[i]) randomType = (QuestType)i;
        }
        for (var i = 0; i < allRanks.Length; i++)
        {
            if (randomValue < config.RankCdf[i]) randomRank = (Rank)i;
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
}
