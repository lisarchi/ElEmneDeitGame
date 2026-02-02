using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _scene;

    public void OnPlayButtonClicked()
    {
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeToScene(_scene);
        else
            Debug.LogWarning("SceneFader не найден!");
    }

    public void OnNewGameButtonClicked()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.DeleteSave();
        else
            Debug.LogWarning("SaveManager не найден! Сохранение не удалено");

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeToScene(_scene);
    }
}
