using System.Collections.Generic;
using UnityEngine;

public class GuildManager : MonoBehaviour
{
    public static GuildManager Instance { get; private set; }

    public QuestConfig QuestConfig;
    [SerializeField] List<GameObject> backgroundUI;

    void Awake()
    {
        Instance = this;
    }

    public void ShowBackgroundUI()
    {
        foreach (var ui in backgroundUI)
        {
            ui.SetActive(true);
        }
    }

    public void HideBackgroundUI()
    {
        foreach (var ui in backgroundUI)
        {
            ui.SetActive(false);
        }
    }
}

public abstract class GuildUIWindow : MonoBehaviour, IWindow
{
    public virtual void ShowWindow()
    {
        gameObject.SetActive(true);
        GuildManager.Instance.HideBackgroundUI();
    }

    public virtual void HideWindow()
    {
        gameObject.SetActive(false);
        GuildManager.Instance.ShowBackgroundUI();
    }
}
