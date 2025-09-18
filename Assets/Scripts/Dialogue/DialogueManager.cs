using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    public List<Dialogue> AllDialogue = new();
    public List<DialogueNode> AllDialogueNode = new();
    readonly Dictionary<string, Dialogue> dialogueDictionary = new();
    readonly Dictionary<string, DialogueNode> dialogueNodeDictionary = new();
    public NPCNameDictionary NPCNameDictionary;
    
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] GameObject choicePrefab;
    [SerializeField] Transform choiceContainer;

    const float Delay = 0.03f;

    Dialogue currentDialogue;
    DialogueNode currentNode;
    Coroutine typeRoutine;

    void Awake()
    {
        Instance = this;
        
        InitializeDictionaries();
        
        gameObject.SetActive(false);
    }

    void InitializeDictionaries()
    {
        foreach (var dialogue in AllDialogue)
        {
            dialogueDictionary.Add(dialogue.ID, dialogue);
        }
        foreach (var node in AllDialogueNode)
        {
            dialogueNodeDictionary.Add(node.ID, node);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
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
        gameObject.SetActive(true);
        currentDialogue = dialogue;
        NextNode(dialogue.HeadNodeID);
    }
    
    void CloseDialogue()
    {
        gameObject.SetActive(false);
    }

    #region Node
    
    void OnNodeEnd()
    {
        switch (currentNode)
        {
            case DialogueLineNode lineNode:
                // Start next node
                NextNode(lineNode.NextNodeID);
                break;
            case DialogueChoiceNode choiceNode:
                // show choices
                CreateChoices(choiceNode.Choices);
                break;
        }
    }

    void NextNode(string id)
    {
        SetNode(id);
        StartNode();
    }
    
    void SetNode(string nodeID)
    {
        currentNode = dialogueNodeDictionary.GetValueOrDefault(nodeID);
        print($"set node: {currentNode.ID}");
    }

    void StartNode()
    {
        text.text = string.Empty;
        speakerNameText.text = NPCNameDictionary.Dict.TryGetValue(currentNode.SpeakerID, out var speakerName) ? speakerName : currentNode.SpeakerID;
        typeRoutine = StartCoroutine(TypeText(currentNode.Text));
    }

    #endregion

    #region Choice
    
    void CreateChoices(List<Choice> choices)
    {
        foreach (var choice in choices)
        {
            var choiceInstance = Instantiate(choicePrefab, choiceContainer);
            choiceInstance.GetComponentInChildren<TextMeshProUGUI>().text = choice.Text;
            
            var onClick = choiceInstance.GetComponentInChildren<Button>().onClick;
            onClick.AddListener(() => OnChoice(choice));
        }
    }

    void OnChoice(Choice choice)
    {
        ActivateEffects(choice.Effects);
        if (!string.IsNullOrEmpty(choice.NextNodeID))
        {
            NextNode(choice.NextNodeID);
        }
    }

    void ActivateEffects(List<ChoiceEffect> effects)
    {
        foreach (var id in effects)
        {
            ActivateEffect(id);
        }
    }
    
    void ActivateEffect(ChoiceEffect effect)
    {
        if (effect.ID.Equals("Dialogue_End"))
        {
            CloseDialogue();
        }
    }
    
    #endregion
}
