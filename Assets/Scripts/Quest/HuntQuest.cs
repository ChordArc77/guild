using UnityEngine;

[CreateAssetMenu(fileName = "Hunt Quest", menuName = "Quest/Hunt Quest")]
public class HuntQuest : Quest
{
    public override QuestType Type => QuestType.Hunt;
}
