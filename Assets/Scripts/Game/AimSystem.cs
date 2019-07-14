using UnityEngine;

public class AimSystem : SingletonComponent<AimSystem>
{
	[SerializeField]
	private LayerMask m_layerMask;

	[SerializeField]
	private float m_maxDist = 100f;

	private bool m_hasPosition;
	private Vector3 m_position;

	private void Update()
	{
		var targetRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		if (m_hasPosition = Physics.Raycast(targetRay, out var hit, m_maxDist, m_layerMask))
		{
			m_position = hit.point;
		}
	}

	public bool TryGetMousePosition(out Vector3 position)
	{
		position = m_position;

		return m_hasPosition;
	}
}
