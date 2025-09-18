using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WidthController : MonoBehaviour, ILayoutSelfController
{
    [SerializeField] float padding;

    TextMeshProUGUI[] texts;
    RectTransform rt;

    void Awake()
    {
       rt = GetComponent<RectTransform>();
    }

    public void SetLayoutHorizontal()
    {
        texts = GetComponentsInChildren<TextMeshProUGUI>();
        float widest = texts.Aggregate(0f, (current, text) => Mathf.Max(text.preferredWidth, current));
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widest + padding * 2);
    }

    public void SetLayoutVertical()
    {

    }
}
