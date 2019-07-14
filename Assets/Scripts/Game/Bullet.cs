using UnityEngine;

public class Bullet : PoolableObject
{
	public static event System.Action<Vector3> onCollision;

	private float m_maxLifeTime = 5f;

	private float m_velocity;
	private float m_speed;
	private Vector3 m_direction;
	private Vector3 m_startPos;
	private float m_time;
	private float m_lifeTime;

	public void Init(Vector3 direction, float velocity, float speed)
	{
		m_direction = direction;
		m_velocity = velocity;
		m_speed = speed;
		m_startPos = transform.position;
		m_lifeTime = m_maxLifeTime * m_speed;
		m_time = 0;
	}

	private void Update()
	{
		m_time += Time.deltaTime * m_speed;

		if (m_time > m_lifeTime)
		{
			Destroy(gameObject);

			return;
		}

		transform.position = m_startPos + BallisticMath.GetPosition(m_direction, m_velocity, m_time);
	}

	private void OnCollisionEnter(Collision collision)
	{
		onCollision?.Invoke(collision.GetContact(0).point);

		BackToPool();
	}
}