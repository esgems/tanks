using EntityConfigs;
using UnityEngine;

namespace BaseEntities
{
	public abstract class BaseWeapon : MonoBehaviour
	{
		public abstract void Init(BaseTank tank, WeaponConfig config);
	}
}