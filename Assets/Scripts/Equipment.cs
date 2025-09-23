using UnityEngine;

public abstract class Equipment : ScriptableObject
{
    public string Name;
    public EquipmentType Type;
    public string Description;
}

public enum EquipmentType
{
    Head,
    Chest,
    Arm,
    Hand,
    Leg,
    Foot
}