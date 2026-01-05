using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance;

    [Header("UI Image должен растягиваться на весь экран")]
    public Image blackPanel;

    [Header("Длительность затемнения/появления")]
    public float fadeDuration = 2f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Панель полностью черная в начале
            SetAlpha(1f);

            // Fade In первой сцены
            StartCoroutine(Fade(1f, 0f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Установить альфу панели
    private void SetAlpha(float a)
    {
        Color c = blackPanel.color;
        c.a = a;
        blackPanel.color = c;
    }

    // Плавное изменение альфа
    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float a = Mathf.Lerp(from, to, t / fadeDuration);
            SetAlpha(a);
            yield return null;
        }
        SetAlpha(to);
    }

    // Переход на сцену по индексу с плавным затемнением
    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeAndLoad(sceneIndex));
    }

    private IEnumerator FadeAndLoad(int sceneIndex)
    {
        // Затемнение
        yield return StartCoroutine(Fade(0f, 1f));

        // Асинхронная загрузка сцены по индексу
        yield return SceneManager.LoadSceneAsync(sceneIndex);

        // Появление
        yield return StartCoroutine(Fade(1f, 0f));
    }
}