using UnityEngine;

public class ParalaxBackground : MonoBehaviour
{
    [SerializeField] private Camera _cam;

    [SerializeField] private float _parallax;
    private float _startPositionX;
    private float _startPositionY;

    private void Start()
    {
        _startPositionX = transform.position.x;
        _startPositionY = transform.position.y;
    }

    private void FixedUpdate()
    {
        float distX = _cam.transform.position.x * (1 - _parallax);
        float distY = _cam.transform.position.y * (1 - _parallax);
        transform.position = new Vector3(_startPositionX + distX, _startPositionY + distY, transform.position.z);
    }
}
