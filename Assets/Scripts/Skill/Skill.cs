using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string Name;
    public string Description;
    public SkillType Type;
}

public enum SkillType
{
    MeleeAttack,
    MagicAttack,
    Passive
}
