using UnityEngine;

[CreateAssetMenu(fileName = "Gather Quest", menuName = "Quest/Gather Quest")]
public class GatherQuest : Quest
{
    public override QuestType Type => QuestType.Gather;
}