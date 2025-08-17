using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // �������� ����� � �����
    public string gameSceneName = "SampleScene";

    // ������ �� ������ �������� (���� ����)
    public GameObject settingsPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }

    public void OpenSettings()
    {
        // �������� ������� ���� � ���������� ���������
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