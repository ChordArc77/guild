using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "NPC Data/NPC Name Dictionary")]
public class NPCNameDictionary : ScriptableObject
{
    [SerializeField] List<NPCName> list = new();
    
    Dictionary<string, string> dict;
    public Dictionary<string, string> Dict
    {
        get
        {
            if (dict != null) return dict;
            
            Debug.Log($"create name dictionary");
            dict = new Dictionary<string, string>();
            foreach (var entry in list)
            {
                dict.Add(entry.ID, entry.Name);
            }
            return dict;
        }
    }
}
