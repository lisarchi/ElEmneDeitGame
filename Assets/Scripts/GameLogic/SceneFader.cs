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

            SetAlpha(1f);
            StartCoroutine(Fade(1f, 0f));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void SetAlpha(float a)
    {
        Color c = blackPanel.color;
        c.a = a;
        blackPanel.color = c;
    }

    private IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            SetAlpha(Mathf.Lerp(from, to, t / fadeDuration));
            yield return null;
        }
        SetAlpha(to);
    }

    public void FadeToScene(int sceneIndex)
    {
        StartCoroutine(FadeAndLoad(sceneIndex));
    }

    private IEnumerator FadeAndLoad(int sceneIndex)
    {
        // Затемнение
        yield return StartCoroutine(Fade(0f, 1f));

        // Асинхронная загрузка сцены
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
            yield return null;

        // Ждём кадр
        yield return null;

        // 🔹 Применяем сохранение
        ApplySaveIfExists();

        // Появление
        yield return StartCoroutine(Fade(1f, 0f));
    }

    private void ApplySaveIfExists()
    {
        if (!SaveManager.Instance.HasSave()) return;

        SaveData data = SaveManager.Instance.LoadGame();
        if (data == null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        GameObject healthAdapter = GameObject.FindGameObjectWithTag("PlayerHealth");
        if (healthAdapter == null) return;

        HealthSaveAdapter adapter = healthAdapter.GetComponent<HealthSaveAdapter>();
        if (adapter == null) return;

        PlayerController pc = player.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (CheckpointManager.Instance != null)
            {
                CheckpointManager.Instance.RegisterCheckpoint(data.checkpointID);
                player.transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
            }

            if (adapter != null)
                adapter.SetHealth(data.health);
        }
    }
}