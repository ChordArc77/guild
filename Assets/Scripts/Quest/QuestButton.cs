using UnityEngine;
using UnityEngine.EventSystems;

public class QuestButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Quest Quest;
    [SerializeField] RectTransform gradientCircle;
    bool hovering;

    void Update()
    {
        if (QuestDetailWindow.showing) return;

        if (hovering && Input.GetMouseButtonDown(0))
        {
            // Pop up quest details
            QuestManager.Instance.ShowDetailWindow(Quest);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        gradientCircle.localScale = new Vector3(1.25f, 1.25f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        gradientCircle.localScale = Vector3.one;
    }
}
