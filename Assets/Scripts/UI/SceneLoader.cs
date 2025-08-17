using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 3f; // ��� 3 �������

    private void Start()
    {
        StartCoroutine(LoadGameAfterDelay());
    }

    private IEnumerator LoadGameAfterDelay()
    {
        // ��� ��������� �����
        yield return new WaitForSeconds(delayBeforeLoad);

        // ��������� ������� ����� ������� ��������
        SceneManager.LoadScene("SampleScene");
    }
}


    //void Start()
    //{
    //    // ��������� ����������� �������� �����
    //    StartCoroutine(LoadSceneAsync());
    //}

    //IEnumerator LoadSceneAsync()
    //{
    //    // ��������� ����� ����������
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");

    //    // ��������� �������������� ��������� �����
    //    operation.allowSceneActivation = false;

    //    while (operation.progress < 0.9f)
    //    {
    //        yield return null;
    //    }
    //}
