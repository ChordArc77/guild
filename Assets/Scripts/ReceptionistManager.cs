using UnityEngine;

public class ReceptionistManager : MonoBehaviour
{
    public static ReceptionistManager Instance { get; private set; }
    [SerializeField] ReceptionistWindow window;
    [SerializeField] Dialogue welcomeDialogue;

    void Awake()
    {
        Instance = this;
    }

    public void ShowWindow()
    {
        window.ShowWindow();
        DialogueManager.Instance.StartDialogue(welcomeDialogue);
    }

    public void HideWindow()
    {
        window.HideWindow();
    }
}
