using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue Dictionary", menuName = "Dialogue/Dictionary/Dialogue")]
public class DialogueDictionary : GenericDictionary<string, Dialogue, Dialogue>
{
    protected override string GetKeyFromEntry(Dialogue entry) => entry.ID;
    protected override Dialogue GetValueFromEntry(Dialogue entry) => entry;
}
