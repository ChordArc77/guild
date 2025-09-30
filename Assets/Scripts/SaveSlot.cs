using TMPro;
using UnityEngine;

public class SaveSlot : MonoBehaviour
{
    PlayerStatus playerStatus;

    [SerializeField] TextMeshProUGUI nameText;

    public void SetSlot(PlayerStatus status)
    {
        playerStatus = status;
        RefreshSlot();
    }

    void RefreshSlot()
    {
        nameText.text = playerStatus != null ? playerStatus.Name : "Empty";
    }
}
