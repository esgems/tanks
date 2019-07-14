using UnityEngine;

public class BulletsVFXManager : MonoBehaviour
{
	private const int PoolSize = 5;

	[SerializeField]
	private string m_bulletsVFXPath;

	private ObjectsPool m_pool;

	private void Awake()
	{
		m_pool = new ObjectsPool(m_bulletsVFXPath, PoolSize);

		Bullet.onCollision += BulletCollisionHandler;
	}

	private void OnDestroy()
	{
		Bullet.onCollision -= BulletCollisionHandler;
	}

	private void BulletCollisionHandler(Vector3 point)
	{
		if (m_pool.TryGet(out var obj))
		{
			obj.transform.position = point;
		}
	}
}
