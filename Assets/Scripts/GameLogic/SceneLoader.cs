using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] internal int _scene;

    public void OnPlayButtonClicked()
    {
        SceneFader.Instance.FadeToScene(_scene);
    }

}
