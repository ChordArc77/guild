using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Nodes/Line")]
public class DialogueLineNode : DialogueNode
{
    public string NextNodeID = string.Empty;
    public DialogueEffect[] Effects;
}
