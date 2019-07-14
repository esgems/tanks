using BaseEntities;
using UnityEngine;

namespace EntityConfigs
{
	[CreateAssetMenu(fileName = "WeaponConfig", menuName = "Weapons/WeaponConfig")]
	public class WeaponConfig : ScriptableObject
	{
		public BaseWeapon weaponObject;
		public string bulletPath;
		public float startVelocity;
		public float bulletSpeed;
		public float shotInterval;
		public float horizontalLimit;
		public float verticalMaxLimit;
		public float rotationSpeed;
	}
}