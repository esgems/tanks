using System.Collections;
using UnityEngine;

public class BulletExplosion : PoolableObject
{
	[SerializeField]
	private float m_lifeTime = 2f;

	private void OnEnable()
	{
		StartCoroutine(WaitingCoroutine());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}

	private IEnumerator WaitingCoroutine()
	{
		yield return new WaitForSeconds(m_lifeTime);

		BackToPool();
	}
}
