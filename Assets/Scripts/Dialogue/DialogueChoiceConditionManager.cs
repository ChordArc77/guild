using System.Linq;
using UnityEngine;

public class DialogueChoiceConditionManager : MonoBehaviour
{
    public static bool CheckConditions(ChoiceCondition[] conditions)
    {
        return conditions.All(CheckCondition);
    }

    static bool CheckCondition(ChoiceCondition condition)
    {
        switch (condition.ID)
        {
            case "HasQuest":
                return QuestManager.Instance.HoldingQuests.Count > 0;
            default:
                Debug.LogError($"Unknown condition ID: {condition.ID}");
                return false;
        }
    }
}
