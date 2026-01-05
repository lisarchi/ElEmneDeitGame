using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] internal JoystickHandler _joystick;
    [SerializeField] internal float _moveInput;

    public Vector2 checkpointPos;
    private Rigidbody2D _rb;
    private Flip flip;

    public Animator playerAnimator;

    private void Awake()
    {
        flip = GetComponent<Flip>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        checkpointPos = transform.position;
    }
    private void Update()
    {
        _moveInput = _joystick._inputVector.x;
        //_moveInput = Input.GetAxis("Horizontal");

        playerAnimator.SetFloat("HorizontalMove", Mathf.Abs(_moveInput));
    }
    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;

        if (_rb.linearVelocity.magnitude < _speed)
        {
            _rb.AddForce(_moveInput * Vector2.right * _speed);
        }

        if (_moveInput > 0)
        {
            flip.FlipDirection("right");
        }

        if (_moveInput < 0)
        {
            flip.FlipDirection("left");
        }
    }

    internal void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }
    
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }
    public IEnumerator Respawn(float duration)
    {
        _rb.linearVelocity = new Vector2(0, 0);
        _rb.simulated = false;
        transform.localScale = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;
        transform.localScale = new Vector3(1, 1, 1);
        _rb.simulated = true;
    }
}
