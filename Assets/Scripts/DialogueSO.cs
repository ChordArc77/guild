using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public string[] lines;
    public DialogueOption[] options;
}
