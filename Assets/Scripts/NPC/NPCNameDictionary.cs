using UnityEngine;

[CreateAssetMenu(menuName = "NPC Data/NPC Name Dictionary")]
public class NPCNameDictionary : GenericDictionary<string, string, NPCName>
{
    protected override string GetKeyFromEntry(NPCName entry) => entry.ID;
    protected override string GetValueFromEntry(NPCName entry) => entry.Name;
}
