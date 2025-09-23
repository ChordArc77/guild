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