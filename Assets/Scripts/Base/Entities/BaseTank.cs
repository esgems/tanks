using EntityConfigs;
using UnityEngine;

namespace BaseEntities
{
	public abstract class BaseTank : MonoBehaviour
	{
		private float m_rotationSpeed;
		private Movement m_movement;

		protected abstract void CreateWeapon(WeaponConfig weapon);

		public virtual void Init(TankConfig config)
		{
			m_rotationSpeed = config.rotationSpeed;

			if (config.movementSpeed > 0)
			{
				m_movement = gameObject.AddComponent<Movement>();
				m_movement.Init(config.movementSpeed);
			}

			CreateWeapon(config.weapon);
		}

		public void LookTo(Vector3 direction)
		{
			transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * m_rotationSpeed);
		}
	}
}