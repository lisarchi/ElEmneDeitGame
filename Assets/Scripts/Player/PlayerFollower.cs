using UnityEngine;

public class PlayerFolower : MonoBehaviour
{
	[SerializeField] private float _followSpeed = 2f;
	[SerializeField] private Transform _target;

    private Transform _objectTransform;
    private Vector3 _originalPos;

	void Awake()
	{
		if (_objectTransform == null)
		{
            _objectTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void Start()
	{
		_originalPos = _objectTransform.localPosition;
	}

	private void Update()
	{
		Vector3 Offset = new Vector2(0, 5f);
		Vector3 newPosition = _target.position + Offset;
		transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);
	}
}
