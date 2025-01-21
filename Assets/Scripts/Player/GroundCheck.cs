using UnityEngine;

[ExecuteInEditMode]
public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float _groundedDistance = .15f;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] internal bool _isGrounded;
    public event System.Action Grounded;

    void LateUpdate()
    {
        bool isGroundedNow = _isGrounded;
        _isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _groundedDistance, _whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                _isGrounded = true;
                if (!_isGrounded)
                {
                    Grounded?.Invoke();
                }
            }
        }
    }
}
