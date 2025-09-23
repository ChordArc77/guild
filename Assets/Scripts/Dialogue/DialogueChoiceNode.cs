using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Dialogue/Nodes/Choice")]
public class DialogueChoiceNode : DialogueNode
{
    public Choice[] Choices;
}

[Serializable]
public class Choice
{
    public string Text;
    public ChoiceCondition[] Conditions;
    public DialogueEffect[] Effects;
    public string NextNodeID;
}

[Serializable]
public class ChoiceCondition
{
    public string ID;
}
