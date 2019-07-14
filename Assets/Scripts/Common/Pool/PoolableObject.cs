using UnityEngine;

public class PoolableObject : MonoBehaviour
{
	public ObjectsPool pool { get; set; }

	public bool isActive
	{
		get
		{
			return gameObject.activeSelf;
		}

		set
		{
			gameObject.SetActive(value);
		}
	}

	protected void BackToPool()
	{
		pool.BackToPool(this);
	}
}
