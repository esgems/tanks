using BaseEntities;
using EntityConfigs;
using UnityEngine;

namespace Entities
{
	public class Tank : BaseTank
	{
		[SerializeField]
		private Transform m_gunPoint;

		protected override void CreateWeapon(WeaponConfig config)
		{
			if (config != null && m_gunPoint != null)
			{
				var weaponObject = Instantiate(config.weaponObject, transform).transform;
				weaponObject.localPosition = m_gunPoint.localPosition;
				weaponObject.localRotation = m_gunPoint.localRotation;

				var weapon = weaponObject.GetComponent<BaseWeapon>();
				weapon.Init(this, config);
			}
		}
	}
}