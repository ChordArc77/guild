using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueOptions : MonoBehaviour
{
    [SerializeField] GameObject optionPrefab;

    public void CreateOptions(DialogueOption[] options)
    {
        foreach (var option in options)
        {
            var instance = Instantiate(optionPrefab, transform);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = option.text;
            instance.GetComponent<Button>().onClick.AddListener(() => option.onSelect.Invoke());
        }
    }
}

[CreateAssetMenu(menuName = "ScriptableObjects/DialogueOption", fileName = "DialogueOption")]
public class DialogueOption : ScriptableObject
{
    public string text;
    public UnityEvent onSelect;
}
