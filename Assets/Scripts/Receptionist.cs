using System.Collections.Generic;
using UnityEngine;

public class Receptionist : MonoBehaviour
{
    public static Receptionist Instance { get; private set; }
    
    [SerializeField] GameObject receptionWindow;
    [SerializeField] List<GameObject> objectsToDisable;
    [SerializeField] Dialogue welcomeDialogue;

    void Awake()
    {
        Instance = this;
    }
    
    public void OpenReceptionWindow()
    {
        receptionWindow.SetActive(true);

        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        DialogueManager.Instance.StartDialogue(welcomeDialogue);
    }

    public void CloseReceptionWindow()
    {
        receptionWindow.SetActive(false);

        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(true);
        }
    }
}
