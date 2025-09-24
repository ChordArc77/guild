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

    void HandleBack()
    {
        SceneManager.LoadScene(titleScene.name);
    }

    void HandleSlot(int id)
    {
        SaveLoadManager.SetSlot(id);
        SceneManager.LoadScene(gameScene.name);
    }
}
