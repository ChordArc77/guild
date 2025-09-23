using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Node Dictionary", menuName = "Dialogue/Dictionary/Node")]
public class DialogueNodeDictionary : GenericDictionary<string, DialogueNode, DialogueNode>
{
    protected override string GetKeyFromEntry(DialogueNode entry) => entry.ID;
    protected override DialogueNode GetValueFromEntry(DialogueNode entry) => entry;
}
