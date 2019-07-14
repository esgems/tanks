using UnityEngine;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T m_instance;

	public static T instance
	{
		get
		{
			return m_instance;
		}
	}

	private void Awake()
	{
		if (m_instance != null)
		{
			Destroy(this.gameObject);

			return;
		}

		m_instance = this as T;

		DontDestroyOnLoad(gameObject);
	}
}
