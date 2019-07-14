using BaseEntities;
using BaseUI;
using EntityConfigs;
using GameUI;
using UnityEngine;

namespace Game
{
	public class GameManager : MonoBehaviour
	{
		[SerializeField]
		private string m_tankConfigsPath;

		private TankConfig[] m_tankConfigs;

		private void Start()
		{
			LoadTanks();

			var tank = CreateRandomTank();

			InitCamera(tank.transform);
			InitUI();
		}

		private void LoadTanks()
		{
			m_tankConfigs = Resources.LoadAll<TankConfig>(m_tankConfigsPath);
		}

		private BaseTank CreateRandomTank()
		{
			var randomNum = Random.Range(0, m_tankConfigs.Length);
			var tankConfig = m_tankConfigs[randomNum];

			return TankConfig.CreateTank(tankConfig);
		}

		private void InitCamera(Transform target)
		{
			var lookTarget = FindObjectOfType<LookTarget>();

			if (lookTarget != null)
			{
				lookTarget.SetObserver(Camera.main.transform);
				lookTarget.SetTarget(target);
			}
		}

		private void InitUI()
		{
			UIManager.instance.Create<AimUIContext>();
			UIManager.instance.Create<HintUIContext>();
		}
	}
}