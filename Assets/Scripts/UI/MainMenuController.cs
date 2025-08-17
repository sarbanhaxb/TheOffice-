using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Название сцены с игрой
    public string gameSceneName = "SampleScene";

    // Ссылка на панель настроек (если есть)
    public GameObject settingsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenSettings()
    {
        // Скрываем главное меню и показываем настройки
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}