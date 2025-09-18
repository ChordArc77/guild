using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float delay;
    [SerializeField] DialogueOptions option;

    readonly Queue<DialogueSO> queue = new();

    DialogueSO current;
    int lineIndex;
    bool isTyping;

    public static Dialogue Instance;

    void Awake()
    {
        Instance = this;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.anyKeyDown)
        {
            if (isTyping)
            {
                StopAllCoroutines();
                text.text = current.lines[lineIndex];
                isTyping = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    public void AddDialogue(DialogueSO dialogue)
    {
        queue.Enqueue(dialogue);
        gameObject.SetActive(true);
        StartDialogue();
    }

    void StartDialogue()
    {
        lineIndex = 0;
        text.text = string.Empty;
        current = queue.Dequeue();
        StartCoroutine(TypeLine(current.lines[lineIndex]));
    }

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
            yield return new WaitForSeconds(delay);
        }
        isTyping = false;
    }

    void NextLine()
    {
        if (lineIndex < current.lines.Length - 1)
        {
            lineIndex++;
            text.text = string.Empty;
            StartCoroutine(TypeLine(current.lines[lineIndex]));
        }
        else
        {
            if (current.options.Length > 0)
            {
                option.CreateOptions(current.options);
            }
            else
            {
                EndDialogue();
            }
        }
    }
}
