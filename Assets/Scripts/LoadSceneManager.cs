using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : MonoBehaviour
{
    [SerializeField] Button[] slots;
    [SerializeField] Button backButton;
    [SerializeField] SceneAsset titleScene;
    [SerializeField] SceneAsset gameScene;

    void Awake()
    {
        for (var i = 0; i < slots.Length; i++)
        {
            var id = i + 1;
            slots[i].onClick.AddListener(() => HandleSlot(id));
        }
        LoadSlots();
        backButton.onClick.AddListener(HandleBack);
    }

    void OnDestroy()
    {
        for (var i = 0; i < slots.Length; i++)
        {
            var id = i + 1;
            slots[i].onClick.RemoveListener(() => HandleSlot(id));
        }
        backButton.onClick.RemoveListener(HandleBack);
    }

    #region Handle

    public void HandleBack()
    {
        SceneManager.LoadScene(titleScene.name);
    }

    void HandleSlot(int id)
    {
        SaveLoadManager.SetSlot(id);
        SceneManager.LoadScene(gameScene.name);
    }

    #endregion

    #region Load

    void LoadSlots()
    {
        for (var i = 0; i < 3; i++)
        {
            LoadSlot(i);
        }
    }

    void LoadSlot(int id)
    {
        SaveLoadManager.SetSlot(id);
        slots[id].GetComponent<SaveSlot>().SetSlot(SaveLoadManager.TryLoadStatus(out var status) ? status : null);
    }

    #endregion
}
