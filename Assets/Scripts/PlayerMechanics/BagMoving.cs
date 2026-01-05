using UnityEngine;

public class BagMoving : MonoBehaviour
{
    private Rigidbody2D _rb;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + Vector2.left * _speed);

        _rb.MoveRotation(_rb.rotation + _rotationSpeed * Time.fixedDeltaTime);
    }
}
