using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] GameObject dialogueWindow;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Transform choiceContainer;

    const float Delay = 0.03f;

    DialogueNode currentNode;
    Coroutine typeRoutine;

    bool isWaitingForChoice;
    readonly List<GameObject> currentChoices = new();

    bool ignoreMouseInput;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (!ignoreMouseInput && (Input.GetMouseButtonDown(0) || Input.anyKeyDown))
        {
            if (typeRoutine != null)
            {
                // Skip typing
                StopCoroutine(typeRoutine);
                text.text = currentNode.Text;
                typeRoutine = null;
            }
            else
            {
                OnNodeEnd();
            }
        }

        if (ignoreMouseInput) ignoreMouseInput = false;

        if (typeRoutine == null && currentNode is DialogueChoiceNode)
        {
            // show choices after finish typing
            OnNodeEnd();
        }
    }

    IEnumerator TypeText(string line)
    {
        foreach (var c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(Delay);
        }
        typeRoutine = null;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueWindow.SetActive(true);
        NextNode(dialogue.HeadNodeID);
        ignoreMouseInput = true;
    }

    public void CloseDialogue()
    {
        dialogueWindow.SetActive(false);
        currentNode = null;
    }

    #region Node

    void OnNodeEnd()
    {
        switch (currentNode)
        {
            case DialogueLineNode lineNode:
                // Start next node
                DialogueEffectManager.ActivateEffects(lineNode.Effects);
                NextNode(lineNode.NextNodeID);
                break;
            case DialogueChoiceNode choiceNode:
                // show choices
                if (isWaitingForChoice) break;
                CreateChoices(choiceNode.Choices);
                isWaitingForChoice = true;
                break;
        }
    }

    void NextNode(string id)
    {
        if (TrySetNode(id))
        {
            StartNode();
        }
    }

    bool TrySetNode(string nodeID)
    {
        currentNode = DictionaryManager.Instance.GetDialogueNodeFromID(nodeID);
        return currentNode != null;
    }

    void StartNode()
    {
        text.text = string.Empty;
        speakerNameText.text = DictionaryManager.Instance.GetNPCNameFromID(currentNode.SpeakerID);
        typeRoutine = StartCoroutine(TypeText(currentNode.Text));
    }

    #endregion

    #region Choice

    void CreateChoices(Choice[] choices)
    {
        foreach (var choice in choices)
        {
            var choiceInstance = Instantiate(choicePrefab, choiceContainer);
            choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choice.Text;

            var button = choiceInstance.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => OnChoice(choice));
            button.interactable = DialogueChoiceConditionManager.CheckConditions(choice.Conditions);

            currentChoices.Add(choiceInstance);
        }
    }

    void OnChoice(Choice choice)
    {
        isWaitingForChoice = false;

        ApplyEffects(choice.Effects);

        if (!string.IsNullOrEmpty(choice.NextNodeID))
        {
            NextNode(choice.NextNodeID);
        }

        foreach (var choiceInstance in currentChoices)
        {
            Destroy(choiceInstance);
        }
        currentChoices.Clear();
    }

    #endregion

    #region Effect

    void ApplyEffects(DialogueEffect[] effects)
    {
        DialogueEffectManager.ActivateEffects(effects);
    }

    #endregion
}
