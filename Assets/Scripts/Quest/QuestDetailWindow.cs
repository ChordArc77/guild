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
    [SerializeField] TextMeshProUGUI rewardText;
    [SerializeField] TextMeshProUGUI giverText;

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
        SetText(quest);
        showing = true;
        BackStack.Push(Hide);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        showing = false;
        BackStack.Remove(Hide);
    }

    void SetText(Quest quest)
    {
        nameText.text = quest.Name;
        rankText.text = quest.Rank.GetText();
        typeText.text = quest.Type.GetText();
        contentText.text = quest.Content;
        rewardText.text = quest.Reward.GetText();
        giverText.text = quest.Giver;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
    }
}
