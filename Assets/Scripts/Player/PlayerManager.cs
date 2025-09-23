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
        playerStatus = SaveLoadManager.TryLoad(out PlayerStatus status) ? status : null;
    }

    public void SavePlayerStatus()
    {
        SaveLoadManager.Save(playerStatus);
    }
}
