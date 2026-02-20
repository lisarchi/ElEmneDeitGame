using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator playerAnimator;
    public HealthSaveAdapter healthAdapter;
    
    [Header("Movement")]
    [SerializeField] private float _speed = 100f;
    [SerializeField] internal JoystickHandler _joystick;
    [SerializeField] internal float _moveInput;

    private Rigidbody2D _rb;
    private Flip _flip;
    internal Vector2 checkpointPos;

    private void Awake()
    {
        _flip = GetComponent<Flip>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
         StartCoroutine(ApplySavedCheckpoint());
    }

    private void Update()
    {
        _moveInput = _joystick._inputVector.x;
        playerAnimator.SetFloat("HorizontalMove", Mathf.Abs(_moveInput));
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        if (_rb.linearVelocity.magnitude < _speed)
        {
            _rb.AddForce(_moveInput * Vector2.right * _speed);
        }

        if (_moveInput > 0) _flip.FlipDirection("right");
        if (_moveInput < 0) _flip.FlipDirection("left");
    }

    internal void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    public void UpdateCheckpoint(Vector2 pos, int checkpointId)
    {
        checkpointPos = pos;
        if (CheckpointManager.Instance != null)
            CheckpointManager.Instance.RegisterCheckpoint(checkpointId);
    }

    public IEnumerator Respawn(float duration)
    {
        _rb.linearVelocity = Vector2.zero;
        _rb.simulated = false;

        transform.localScale = Vector3.zero;

        yield return new WaitForSeconds(duration);

        transform.position = checkpointPos;

        HealthSaveAdapter healthAdapter = GetComponent<HealthSaveAdapter>();
        if (healthAdapter != null)
        {
            healthAdapter.SetHealth(5);
        }

        transform.localScale = Vector3.one;
        _rb.simulated = true;
    }

    private IEnumerator ApplySavedCheckpoint()
    {
        yield return new WaitUntil(() => CheckpointManager.Instance != null);

        checkpointPos = CheckpointManager.Instance.GetLastCheckpointPosition();
        transform.position = checkpointPos;

        if (healthAdapter != null && SaveManager.Instance.HasSave())
        {
            SaveData data = SaveManager.Instance.LoadGame();
            healthAdapter.SetHealth(data.health);
        }
    }
}
