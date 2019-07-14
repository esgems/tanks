using BaseEntities;
using UnityEngine;

namespace EntityConfigs
{
	[CreateAssetMenu(fileName = "TankConfig", menuName = "Tanks/TankConfig")]
	public class TankConfig : ScriptableObject
	{
		public BaseTank tankObject;
		public WeaponConfig weapon;
		public float movementSpeed;
		public float rotationSpeed;

		public static BaseTank CreateTank(TankConfig config)
		{
			if (config == null || config.tankObject == null)
			{
				return null;
			}

			var tank = Instantiate(config.tankObject);
			tank.Init(config);

			return tank.GetComponent<BaseTank>();
		}
	}
}