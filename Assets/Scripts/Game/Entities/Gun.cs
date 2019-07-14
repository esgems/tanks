using BaseEntities;
using EntityConfigs;
using UnityEngine;

namespace Entities
{
	public class Gun : BaseWeapon
	{
		private const int PoolSize = 5;

		[SerializeField]
		private Transform m_gun;

		[SerializeField]
		private TrajectoryRenderer m_trajectoryRenderer;

		[SerializeField]
		private LayerMask m_groundlayer;

		private float m_bulletVelocity;
		private float m_bulletSpeed;
		private float m_shotInterval;
		private float m_horizontalAngleLimit;
		private float m_verticalAngleLimit;
		private float m_rotationSpeed;

		private bool m_lowMode;
		private float m_shotTime;
		private BaseTank m_owner;
		private Transform m_ownerTransform;

		private ObjectsPool m_bulletsPool;

		public override void Init(BaseTank owner, WeaponConfig config)
		{
			m_owner = owner;
			m_ownerTransform = owner.transform;
			m_bulletVelocity = config.startVelocity;
			m_bulletSpeed = config.bulletSpeed;
			m_shotInterval = config.shotInterval;
			m_horizontalAngleLimit = config.horizontalLimit;
			m_verticalAngleLimit = config.verticalMaxLimit;
			m_rotationSpeed = config.rotationSpeed;

			m_bulletsPool = new ObjectsPool(config.bulletPath, PoolSize);
		}

		private void Shot(Vector3 direction, float velocity)
		{
			if (Time.time - m_shotTime < m_shotInterval)
			{
				return;
			}

			m_shotTime = Time.time;

			if (m_bulletsPool.TryGet(out var bullet))
			{
				bullet.transform.position = transform.position;
				var bulletComponent = bullet.GetComponent<Bullet>();

				if (bulletComponent != null)
				{
					bulletComponent.Init(direction, velocity, m_bulletSpeed);
				}
			}
		}

		private void Update()
		{
			if (Input.GetButtonDown("AimMode"))
			{
				m_lowMode = !m_lowMode;
			}

			var availableShot = false;

			if (AimSystem.instance.TryGetMousePosition(out var targetPos))
			{
				var startPos = transform.position;

				if (BallisticMath.TryGetVerticalAngle(startPos, targetPos, m_bulletVelocity, m_lowMode, out var verticalAngle))
				{
					var direction = (targetPos - transform.position).normalized;
					direction.y = 0;

					var verticalAngleClamped = Mathf.Clamp(verticalAngle, 0f, m_verticalAngleLimit);

					m_gun.localRotation = Quaternion.Euler(-verticalAngleClamped, 0, 0);

					var horizontalAvailable = GetHorizontalAngleAvailable(direction);

					if (horizontalAvailable)
					{
						transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * m_rotationSpeed);

						availableShot = Mathf.Abs(verticalAngleClamped - verticalAngle) < 0.1f;

						if (Input.GetButtonDown("Fire1") && availableShot)
						{
							Shot(m_gun.forward, m_bulletVelocity);
						}
					}
					else
					{
						m_owner.LookTo(direction);
					}

					m_trajectoryRenderer.DrawTrajectory(startPos, m_gun.forward, m_bulletVelocity, targetPos, availableShot);
				}
			}

			if (!availableShot)
			{
				m_trajectoryRenderer.Clear();
			}
		}

		private bool GetHorizontalAngleAvailable(Vector3 direction)
		{
			if (m_horizontalAngleLimit == -1)
			{
				return true;
			}

			return BallisticMath.GetAngle(m_ownerTransform.forward, direction) < m_horizontalAngleLimit;
		}
	}
}