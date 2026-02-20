using UnityEngine;

public class LevelTransition : MonoBehaviour
{
    public int sceneID;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            SceneFader.Instance.FadeToScene(sceneID);
        }
    }
}
