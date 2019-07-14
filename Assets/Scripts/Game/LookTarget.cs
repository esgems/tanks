using UnityEngine;

public class LookTarget : MonoBehaviour
{
	[SerializeField]
	private float _speed;

	[SerializeField]
	private Vector3 _offset;

	[SerializeField]
	private Vector3 _offsetRotation;

	[SerializeField]
	private Transform _observer;

	[SerializeField]
	private Transform _target;

	public void Init(Transform observer, Transform target)
	{
		_observer = observer;
		_target = target;
	}

	private void Update()
    {
		if (_observer != null && _target != null)
		{
			var time = Time.deltaTime * _speed;
			_observer.position = Vector3.MoveTowards(_observer.position, _target.position + _offset, time);
			_observer.rotation = Quaternion.Lerp(_observer.rotation, Quaternion.Euler(_offsetRotation), time);
		}
	}
}
