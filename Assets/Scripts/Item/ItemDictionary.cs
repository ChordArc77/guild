using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Dictionary", menuName = "Item/Dictionary")]
public class ItemDictionary : RebuilableDictionary<Item>
{
    public Dictionary<int, Item> Dictionary;

    void OnEnable() => Rebuild();
    void OnValidate() => Rebuild();

    public override void Rebuild()
    {
        if (Entries == null) return;

        foreach (var entry in Entries)
        {
            Dictionary.TryAdd(entry.ID, entry);
        }
    }
}
