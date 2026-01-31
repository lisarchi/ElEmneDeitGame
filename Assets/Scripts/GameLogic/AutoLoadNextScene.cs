using UnityEngine;
using System.Collections;

public class AutoLoadNextScene : MonoBehaviour
{
    [SerializeField] private float _delay = 4f;
    [SerializeField] internal int _sceneIndex;

    private void Start()
    {
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(_delay);
        SceneFader.Instance.FadeToScene(_sceneIndex);
    }
}