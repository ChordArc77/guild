using System.Collections.Generic;
using UnityEngine;

public class DictionaryManager : MonoBehaviour
{
    public static DictionaryManager Instance { get; private set; }

    public DialogueDictionary DialogueDictionary;
    public DialogueNodeDictionary DialogueNodeDictionary;
    public NPCNameDictionary NPCNameDictionary;
    public ItemDictionary ItemDictionary;

    void Awake()
    {
        Instance = this;
    }

    public Dialogue GetDialogueFromID(string ID) => DialogueDictionary.Dictionary.GetValueOrDefault(ID, null);
    public DialogueNode GetDialogueNodeFromID(string ID) => DialogueNodeDictionary.Dictionary.GetValueOrDefault(ID, null);
    public string GetNPCNameFromID(string ID) => NPCNameDictionary.Dictionary.GetValueOrDefault(ID, "ERROR");
    public Item GetItemFromID(int ID) => ItemDictionary.Dictionary.GetValueOrDefault(ID, null);
}
