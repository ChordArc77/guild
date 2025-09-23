using UnityEngine;

[CreateAssetMenu(fileName = "Plant Item", menuName = "Item/Plant")]
public class PlantItem : Item
{
    public override ItemType Type => ItemType.Plant;

    public bool HasPoison;
}
