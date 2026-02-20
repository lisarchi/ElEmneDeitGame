using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private int _sceneID;

    public void OnPlayButtonClicked()
    {
        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeToScene(_sceneID);
        else
            Debug.LogWarning("SceneFader не найден!");
    }

    public void OnNewGameButtonClicked()
    {
        if (SaveManager.Instance != null)
            SaveManager.Instance.DeleteSave();
        else
            Debug.LogWarning("SaveManager не найден! Сохранение не удалено");

        if (CheckpointManager.Instance != null)
            CheckpointManager.Instance.ResetProgress();

        if (SceneFader.Instance != null)
            SceneFader.Instance.FadeToScene(_sceneID);
    }
}
