using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private PlayerController _controller;

    internal void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.Equals("Player"))
        {
            print("death");
            _controller.Die();
        }
    }
}
