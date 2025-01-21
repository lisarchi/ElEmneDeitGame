using UnityEngine;

public class ShowMessage : MonoBehaviour
{
    private float _showTime = 3f;
    private float _alphaDelta;
    private Color _spriteColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.color;
        _alphaDelta = _spriteColor.a + _showTime;
    }

    internal void ShowRandomMessage()
    {
        _spriteColor.a += _alphaDelta * Time.deltaTime;
        _spriteRenderer.color = _spriteColor;

    }
}
