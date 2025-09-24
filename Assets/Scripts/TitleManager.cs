using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button quitButton;
    [SerializeField] string loadSceneName = "LoadScene";

    void Awake()
    {
        startButton.onClick.AddListener(HandleStart);
        quitButton.onClick.AddListener(HandleQuit);
    }

    void OnDestroy()
    {
        startButton.onClick.RemoveListener(HandleStart);
        quitButton.onClick.RemoveListener(HandleQuit);
    }

    void HandleStart()
    {
        SceneManager.LoadScene(loadSceneName);
    }

    void HandleQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
