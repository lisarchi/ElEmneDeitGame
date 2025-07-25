using UnityEngine;

public class DeleteElements : MonoBehaviour
{
    private float _showTime = 1f;
    private float _alphaDelta;
    private Color _spriteColor;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteColor = _spriteRenderer.color;
        _alphaDelta = _spriteColor.a / _showTime;
    }

    internal void DeleteElement()
    {
        _spriteColor.a -= _alphaDelta * Time.deltaTime;
        _spriteRenderer.color = _spriteColor;

        if (_spriteColor.a <= 0)
            Destroy(gameObject);

    }
    public void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            DeleteElement();
        }
    }
}
