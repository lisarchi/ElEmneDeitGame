using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public int _scene;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            SceneFader.Instance.FadeToScene(_scene);
        }
    }
}
