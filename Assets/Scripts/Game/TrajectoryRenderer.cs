using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
	[SerializeField]
	private LineRenderer m_lineRenderer;

	[SerializeField]
	private int m_maxSize = 200;

	[SerializeField]
	private float m_accuracy = 2;

	public void Clear()
	{
		if (m_lineRenderer != null)
		{
			m_lineRenderer.positionCount = 0;
			m_lineRenderer.SetPositions(new Vector3[0]);
		}
	}

	public void DrawTrajectory(Vector3 startPos, Vector3 direction, float velocity, Vector3 targetPos)
	{
		if (m_lineRenderer == null)
		{
			return;
		}

		Clear();

		var dt = 0f;
		var positions = new List<Vector3>();
		var count = 0;
		var position = startPos;

		while (Vector3.Distance(position, targetPos) > 0.5f && count++ < m_maxSize)
		{
			dt += Time.deltaTime * m_accuracy;

			position = startPos + BallisticMath.GetPosition(direction, velocity, dt);

			if (Physics.CheckSphere(position, 0.1f))
			{
				break;
			}

			positions.Add(position);
		}

		m_lineRenderer.positionCount = positions.Count;
		m_lineRenderer.SetPositions(positions.ToArray());
	}
}
