using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float _followSpeed = 2f;
	[SerializeField] private Transform _target;
	
	private Transform _camTransform;
	private Vector3 _originalPos;

	void Awake()
	{
		Cursor.visible = false;
		if (_camTransform == null)
		{
			_camTransform = GetComponent(typeof(Transform)) as Transform;
		}
	}

	void OnEnable()
	{
		_originalPos = _camTransform.localPosition;
	}

	private void Update()
	{
		Vector3 Offset = new Vector3(4, 1.5f, 0);
		Vector3 newPosition = _target.position + Offset;
		newPosition.z = -10;
		transform.position = Vector3.Slerp(transform.position, newPosition, _followSpeed * Time.deltaTime);
	}
}
