using System.Collections.Generic;
using UnityEngine;

public class Receptionist : MonoBehaviour
{
    [SerializeField] GameObject receptionWindow;
    [SerializeField] List<GameObject> objectsToDisable;
    [SerializeField] Dialogue welcomeDialogue;

    public void OpenReceptionWindow()
    {
        receptionWindow.SetActive(true);

        foreach (var obj in objectsToDisable)
        {
            obj.SetActive(false);
        }

        // DialogueManager.Instance.AddDialogue(welcomeDialogue);
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
