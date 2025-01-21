using UnityEngine;

public class ShowMonsterEyes : MonoBehaviour
{
    private float _showTime = 0.3f;
    private float _alphaDelta;
    private Color _spriteColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.color;
        _alphaDelta = _spriteColor.a / _showTime;
    }

    internal void ShowEyes()
    {
        _spriteColor.a -= _alphaDelta * Time.deltaTime;
        _spriteRenderer.color = _spriteColor;

        if (_spriteColor.a <=0)
            Destroy(gameObject);
        
    }
}
