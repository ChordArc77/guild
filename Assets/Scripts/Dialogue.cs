using System;
using System.Collections.Generic;
using UnityEngine;

#region Dialogue

[CreateAssetMenu(menuName = "Dialogue/New Dialogue")]
public class Dialogue : ScriptableObject
{
    public string ID;
    public string HeadNodeID;
}

#endregion

#region Node

public abstract class DialogueNode : ScriptableObject
{
    public string ID;
    public string Text;
}

[CreateAssetMenu(menuName = "Dialogue/Nodes/Line")]
public class DialogueLineNode : DialogueNode
{
    public string SpeakerID;
    public string NextNodeID;
}

#region ChoiceNode

[CreateAssetMenu(menuName = "Dialogue/Nodes/Choice")]
public class DialogueChoiceNode : DialogueNode
{
    public List<Choice> Choices;
}

[Serializable]
public class Choice
{
    public string Text;
    public List<ChoiceCondition> Conditions;
    public List<ChoiceEffect> Effects;
    public string NextNodeID;
}

[Serializable]
public class ChoiceCondition
{
    public string ID;
}

[Serializable]
public class ChoiceEffect
{
    public string ID;
    public List<string> Args;
}

#endregion

#endregion