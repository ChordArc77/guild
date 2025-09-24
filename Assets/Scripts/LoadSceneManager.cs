using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] Button[] slots;
    [SerializeField] Button backButton;
    [SerializeField] string titleSceneName = "TitleScene";
    [SerializeField] string gameSceneName = "GuildScene"; // change this someday

    void Awake()
    {
        for (var i = 0; i < slots.Length; i++)
        {
            var id = i + 1;
            slots[i].onClick.AddListener(() => HandleSlot(id));
        }
        backButton.onClick.AddListener(HandleBack);
    }

    void OnDestroy()
    {
        foreach (var slot in slots)
        {
            slot.onClick.RemoveAllListeners();
        }
        backButton.onClick.RemoveAllListeners();
    }

    void HandleBack()
    {
        SceneManager.LoadScene(titleSceneName);
    }

    void HandleSlot(int id)
    {
        SaveLoadManager.SetSlot(id);
        SceneManager.LoadScene(gameSceneName);
    }
}
