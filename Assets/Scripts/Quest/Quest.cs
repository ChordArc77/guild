using System;
using UnityEditor;
using UnityEngine;

public abstract class Quest : ScriptableObject
{
    public string Name;
    public Rank Rank;
    public abstract QuestType Type { get; }
    public QuestReward Reward;
    public string Content;
    public int TargetID;
    public int Amount;
    public string Giver;
    public int Due; // change this to Date?
}

public enum QuestType
{
    Hunt,
    Gather
}

[Serializable]
public class QuestReward
{
    public QuestRewardType Type;
    public int Amount;
    public int ItemID;
}

public enum QuestRewardType
{
    Money,
    Item
}

[CustomEditor(typeof(Quest), true)]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.LabelField("Type", ((Quest)target).Type.ToString());
    }
}

public static class QuestExtensions
{
    public static string GetText(this QuestType type)
    {
        return "Type : " + type switch
        {
            QuestType.Hunt => "Hunt",
            QuestType.Gather => "Gather",
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    public static string GetText(this QuestReward reward)
    {
        return "Reward : " + reward.Type switch
        {
            QuestRewardType.Money => $"{reward.Amount} Gold",
            QuestRewardType.Item => $"{reward.Amount} {DictionaryManager.Instance.GetItemFromID(reward.ItemID).Name}",
            _ => throw new ArgumentOutOfRangeException(nameof(reward), reward, null)
        };
    }
}