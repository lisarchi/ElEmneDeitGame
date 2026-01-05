using UnityEngine;

public class DeathZoneDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("DeathZone"))
        {
            Destroy(gameObject);
        }
    }
}
