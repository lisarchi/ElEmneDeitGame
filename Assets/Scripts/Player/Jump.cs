using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpStrength = 60;
    [SerializeField] private GroundCheck _groundCheck;
    public Animator playerAnimator;

    private Rigidbody2D _rb;
    
    public event System.Action Jumped;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Time.timeScale == 0)
            return;
        if (Input.GetKeyDown(KeyCode.Space) && _groundCheck._isGrounded)
        {
            _rb.AddForce(transform.up * _jumpStrength, ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
            if (!_groundCheck || _groundCheck._isGrounded)
        {
            playerAnimator.SetBool("IsJumped", true);
        }
        else
        {
            playerAnimator.SetBool("IsJumped", false);
        }
    }

    public void OnJumpButtonDown()
    {
        if (!_groundCheck || _groundCheck._isGrounded)
        {
            _rb.AddForce(transform.up * _jumpStrength, ForceMode2D.Impulse);
            Jumped?.Invoke();
        }
    }
}
