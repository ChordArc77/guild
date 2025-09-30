using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    PlayerStatus playerStatus;

    void Awake()
    {
        Instance = this;
    }

    public void LoadPlayerStatus()
    {
        playerStatus = SaveLoadManager.TryLoadStatus(out var status) ? status : null;
    }

    public void SavePlayerStatus()
    {
        SaveLoadManager.SaveStatus(playerStatus);
    }
}
