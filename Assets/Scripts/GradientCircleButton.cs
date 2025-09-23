using UnityEngine;
using UnityEngine.Events;

public class GradientCircleButton : MonoBehaviour
{
    [SerializeField] float hoverRadius;
    [SerializeField] bool onWorld;
    [SerializeField] UnityEvent onClick;

    public bool log;

    bool isHovering;

    readonly Vector3 normalScale = new(0.1f, 0.1f, 1f);
    readonly Vector3 hoverScale = new(0.15f, 0.15f, 0.1f);

    void Update()
    {
        CheckHovering();

        transform.localScale = isHovering ? hoverScale : normalScale;

        if (Input.GetMouseButtonDown(0) && isHovering)
        {
            onClick.Invoke();
        }
    }

    void CheckHovering()
    {
        float mouseDistance = Vector2.Distance(onWorld ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : Input.mousePosition, transform.position);
        isHovering = mouseDistance < hoverRadius;
        if (log)
        {
            print($"distance: {mouseDistance}");
        }
    }
}
