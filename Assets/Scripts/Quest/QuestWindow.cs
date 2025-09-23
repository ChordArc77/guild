using UnityEngine;

public class QuestWindow : GuildUIWindow
{
    [SerializeField] GameObject questPrefab;
    [SerializeField] Transform[] questSpawnPos;

    public override void ShowWindow()
    {
        base.ShowWindow();
        PopulateQuests();
        BackStack.Push(HideWindow);
    }

    void PopulateQuests()
    {
        for (var i = 0; i < QuestManager.Instance.CurrentQuests.Count; i++)
        {
            var instance = Instantiate(questPrefab, questSpawnPos[i]);
            instance.GetComponent<QuestButton>().Quest = QuestManager.Instance.CurrentQuests[i];
        }
    }
}
