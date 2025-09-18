using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI speakerNameText;
    [SerializeField] GameObject optionPrefab;
    [SerializeField] Transform optionContainer;

    const float Delay = 0.03f;

    bool isTyping;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
    //     {
    //         if (isTyping)
    //         {
    //             StopAllCoroutines();
    //             text.text = current.Lines[lineIndex];
    //             isTyping = false;
    //         }
    //         else
    //         {
    //             NextLine();
    //         }
    //     }
    // }

    // public void AddDialogue(Dialogue dialogue)
    // {
    //     queue.Enqueue(dialogue);
    //     gameObject.SetActive(true);
    //     StartDialogue();
    // }
    //
    // void StartDialogue()
    // {
    //     lineIndex = 0;
    //     text.text = string.Empty;
    //     current = queue.Dequeue();
    //     // StartCoroutine(TypeLine(current.Lines[lineIndex]));
    // }

    public void EndDialogue()
    {
        gameObject.SetActive(false);
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        foreach (var c in line)
        {
            text.text += c;
            yield return new WaitForSeconds(Delay);
        }
        isTyping = false;
    }

    // void NextLine()
    // {
    //     // if (lineIndex < current.Lines.Length - 1)
    //     {
    //         lineIndex++;
    //         text.text = string.Empty;
    //         // StartCoroutine(TypeLine(current.Lines[lineIndex]));
    //     }
    //     // else
    //     {
    //         if (current.Options.Length > 0)
    //         {
    //             CreateOptions(current.Options);
    //         }
    //         else
    //         {
    //             EndDialogue();
    //         }
    //     }
    // }

    // void CreateOptions(DialogueOption[] options)
    // {
    //     foreach (var option in options)
    //     {
    //         var instance = Instantiate(optionPrefab, optionContainer);
    //         instance.GetComponentInChildren<TextMeshProUGUI>().text = option.Text;
    //         // instance.GetComponent<Button>().onClick.AddListener(() => option.OnSelect.Invoke());
    //     }
    // }
}
