using UnityEngine;

public class CoinRestarter : MonoBehaviour
{
    [SerializeField] private Transform _coinGroup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (Transform child in _coinGroup.GetComponentsInChildren<Transform>(true))
            {
                if (child != _coinGroup)
                    child.gameObject.SetActive(true);
            }
        }
    }
}