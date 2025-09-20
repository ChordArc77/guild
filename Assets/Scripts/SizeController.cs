using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeController : MonoBehaviour, ILayoutSelfController
{
    [SerializeField] RectTransform dialogueBox;
    [SerializeField] float padding;

    TextMeshProUGUI[] texts;
    RectTransform rt;
    Vector2 anchorPos;

    void Awake()
    {
       rt = GetComponent<RectTransform>();
       anchorPos = dialogueBox.TransformVector(new Vector2(dialogueBox.rect.xMax, dialogueBox.rect.yMax));
    }

    public void SetLayoutHorizontal()
    {
        if (!rt) return;
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        var widest = texts.Aggregate(0f, (current, text) => Mathf.Max(text.preferredWidth, current));
        var newWidth = widest + padding * 2;
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, newWidth);
        rt.anchoredPosition = new Vector2(-newWidth / 2, rt.anchoredPosition.y);
    }

    public void SetLayoutVertical()
    {
        if (!rt) return;
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        var heightSum = texts.Sum(text => text.preferredHeight);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, heightSum);
        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, dialogueBox.rect.height + heightSum / 2 + padding);
    }
}
