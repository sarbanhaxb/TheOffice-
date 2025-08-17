using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private float delayBeforeLoad = 3f; // Ждём 3 секунды

    private void Start()
    {
        StartCoroutine(LoadGameAfterDelay());
    }

    private IEnumerator LoadGameAfterDelay()
    {
        // Ждём указанное время
        yield return new WaitForSeconds(delayBeforeLoad);

        // Загружаем игровую сцену обычным способом
        SceneManager.LoadScene("SampleScene");
    }
}


    //void Start()
    //{
    //    // Запускаем асинхронную загрузку сцены
    //    StartCoroutine(LoadSceneAsync());
    //}

    //IEnumerator LoadSceneAsync()
    //{
    //    // Загружаем сцену асинхронно
    //    AsyncOperation operation = SceneManager.LoadSceneAsync("SampleScene");

    //    // Отключаем автоматическую активацию сцены
    //    operation.allowSceneActivation = false;

    //    while (operation.progress < 0.9f)
    //    {
    //        yield return null;
    //    }
    //}
