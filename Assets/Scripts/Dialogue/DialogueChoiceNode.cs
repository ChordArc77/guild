using UnityEngine;
using System;
using System.Collections.Generic;

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
