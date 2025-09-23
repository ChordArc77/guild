using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class QuestDetailWindow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool showing;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI typeText;
    [SerializeField] TextMeshProUGUI contentText;
    [SerializeField] TextMeshProUGUI dueText;
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI giverText;

    Quest showingQuest;
    bool hovering;

    void Update()
    {
        if (!hovering && Input.GetMouseButtonDown(0))
        {
            Hide();
        }
    }

    public void Show(Quest quest)
    {
        gameObject.SetActive(true);
        showingQuest = quest;
        SetText();
        showing = true;
        BackStack.Push(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        showing = false;
        BackStack.Remove(Hide);
    }

    void SetText()
    {
        nameText.text = showingQuest.Name;
        rankText.text = showingQuest.Rank.GetText();
        typeText.text = showingQuest.Type.GetText();
        contentText.text = showingQuest.Content;
        dueText.text = $"Due : {showingQuest.Due} Days";
        rewardText.text = showingQuest.Reward.GetText();
        giverText.text = "Giver : " + showingQuest.Giver;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }

    public void TakeQuest()
    {
        QuestManager.Instance.TakeQuestFromBoard(showingQuest);
        Hide();
    }
}
