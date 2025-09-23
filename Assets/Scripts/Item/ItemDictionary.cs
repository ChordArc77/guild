using UnityEngine;

[CreateAssetMenu(fileName = "Item Dictionary", menuName = "Item/Dictionary")]
public class ItemDictionary : GenericDictionary<int, Item, Item>
{
    protected override int GetKeyFromEntry(Item item) => item.ID;
    protected override Item GetValueFromEntry(Item item) => item;
}
