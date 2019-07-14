using System.Collections.Generic;
using UnityEngine;

public class ObjectsPool
{
	private readonly List<PoolableObject> m_poolObjects = new List<PoolableObject>();
	private int m_capacity;

	private PoolableObject m_object;
	private Vector3 m_normalPosition;
	private Quaternion m_normalRotation;
	private Vector3 m_normalScale;
	private Transform m_container;

	public ObjectsPool(string path, int capacity)
	{
		m_object = Resources.Load<PoolableObject>(path);
		m_capacity = capacity;

		if (m_object == null)
		{
			Debug.LogError($"PooObject not found: {path}");

			return;
		}

		m_normalPosition = m_object.transform.position;
		m_normalRotation = m_object.transform.rotation;
		m_normalScale = m_object.transform.localScale;

		m_container = new GameObject().transform;
		m_container.name = m_object.name + "sPool";
	}

	public bool TryGet(out PoolableObject result)
	{
		for (int i = 0; i < m_poolObjects.Count; ++i)
		{
			var obj = m_poolObjects[i];

			if (obj != null && !obj.isActive)
			{
				obj.isActive = true;

				result = obj;

				return true;
			}
		}

		if (m_poolObjects.Count < m_capacity)
		{
			var newObj = GameObject.Instantiate(m_object, m_container);
			newObj.pool = this;
			m_poolObjects.Add(newObj);

			result = newObj;

			return true;
		}

		result = null;

		return false;
	}

	public void BackToPool(PoolableObject obj)
	{
		if (m_poolObjects.Contains(obj))
		{
			obj.isActive = false;
			obj.transform.position = m_normalPosition;
			obj.transform.rotation = m_normalRotation;
			obj.transform.localScale = m_normalScale;
		}
	}
}
