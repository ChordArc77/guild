using UnityEngine;

public class DialogueEffectManager : MonoBehaviour
{
    public static void ActivateEffects(DialogueEffect[] effects)
    {
        if (effects == null) return;
        foreach (var id in effects)
        {
            ActivateEffect(id);
        }
    }

    static void ActivateEffect(DialogueEffect effect)
    {
        switch (effect.ID)
        {
            case "Dialogue_End":
                DialogueManager.Instance.CloseDialogue();
                break;
            case "Receptionist_Exit":
                ReceptionistManager.Instance.HideWindow();
                break;
            case "Quest_Accept":
                QuestManager.Instance.AcceptHoldingQuest();
                break;
            default:
                Debug.LogError($"Unknown Effect ID: {effect.ID}");
                break;
        }
    }
}
