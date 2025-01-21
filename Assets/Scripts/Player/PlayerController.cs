using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    private Rigidbody2D _rb;
    private Flip flip;

    public Vector2 _moveVector;
    public Animator playerAnimator;

    private void Awake()
    {
        flip = GetComponent<Flip>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _moveVector.x = 0f;
    }
    private void Update()
    {
        playerAnimator.SetFloat("HorizontalMove", Mathf.Abs(_moveVector.x));
        if (Input.GetKeyDown(KeyCode.A))
        {
            _moveVector.x = -1;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            _moveVector.x = 0;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            _moveVector.x = 1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            _moveVector.x = 0;
        }
    }

    public void OnLeftButtonDown()
    {
        _moveVector.x = -1f;
    }
    public void OnRightButtonDown()
    {
        _moveVector.x =1f;
    }
    public void OnButtonUp()
    {
        _moveVector.x = 0f;
    }
    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;

        PlayerMove();

        if (_moveVector.x > 0)
        {
            flip.FlipDirection("right");
        }

        if (_moveVector.x < 0)
        {
            flip.FlipDirection("left");
        }
    }
    private void PlayerMove()
    {
        if (_rb.velocity.magnitude<_speed)
        {
            _rb.AddForce(_moveVector.x * Vector2.right * _speed);
        }
    }
}
