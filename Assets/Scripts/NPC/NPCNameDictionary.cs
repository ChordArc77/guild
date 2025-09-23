using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "NPC Data/NPC Name Dictionary")]
public class NPCNameDictionary : ScriptableObject
{
    public NPCName[] Names;
    public Dictionary<string, string> Dictionary;

    void OnValidate() => Rebuild();
    void OnEnable() => Rebuild();

    void Rebuild()
    {
        if (Names == null) return;

        foreach (var npcName in Names)
        {
            Dictionary.TryAdd(npcName.ID, npcName.Name);
        }
    }
}
