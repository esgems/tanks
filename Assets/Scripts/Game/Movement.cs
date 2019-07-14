using UnityEngine;

public class Movement : MonoBehaviour
{
	private const float SpeedAccuracy = 0.01f;

	private float m_speed;

	public void Init(float speed)
	{
		m_speed = speed;
	}

	private void Update()
	{
		var movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (movement.magnitude > SpeedAccuracy)
		{
			transform.forward = movement.normalized;
			transform.Translate(transform.forward * m_speed, Space.World);
		}
	}
}
