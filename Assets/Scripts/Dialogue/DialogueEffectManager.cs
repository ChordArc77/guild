using System.Collections.Generic;
using UnityEngine;

public class DialogueEffectManager : MonoBehaviour
{
    public static void ActivateEffects(List<ChoiceEffect> effects)
    {
        foreach (var id in effects)
        {
            ActivateEffect(id);
        }
    }

    static void ActivateEffect(ChoiceEffect effect)
    {
        switch (effect.ID)
        {
            case "Dialogue_End":
                DialogueManager.Instance.CloseDialogue();
                break;
            case "Receptionist_Exit":
                Receptionist.Instance.CloseReceptionWindow();
                break;
        }
    }
}
