using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _speed = 100f;
    [SerializeField] internal JoystickHandler _joystick;
    [SerializeField] internal float _moveInput;

    [Header("References")]
    private Rigidbody2D _rb;
    private Flip flip;
    public Animator playerAnimator;
    public HealthSaveAdapter healthAdapter;

    [Header("Checkpoint")]
    public Vector2 checkpointPos;

    private void Awake()
    {
        flip = GetComponent<Flip>();
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
         StartCoroutine(ApplySavedCheckpoint());
    }

    private void Update()
    {
        _moveInput = _joystick._inputVector.x;
        //_moveInput = Input.GetAxis("Horizontal");

        playerAnimator.SetFloat("HorizontalMove", Mathf.Abs(_moveInput));
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0) return;

        if (_rb.linearVelocity.magnitude < _speed)
        {
            _rb.AddForce(_moveInput * Vector2.right * _speed);
        }

        if (_moveInput > 0) flip.FlipDirection("right");
        if (_moveInput < 0) flip.FlipDirection("left");
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
        // Ждём, пока CheckpointManager и Player полностью инициализируются
        yield return new WaitUntil(() => CheckpointManager.Instance != null);

        // Берём последнюю сохранённую позицию
        checkpointPos = CheckpointManager.Instance.GetLastCheckpointPosition();
        transform.position = checkpointPos;

        // Если есть адаптер здоровья, восстанавливаем
        if (healthAdapter != null && SaveManager.Instance.HasSave())
        {
            SaveData data = SaveManager.Instance.LoadGame();
            healthAdapter.SetHealth(data.health);
        }
    }
}
