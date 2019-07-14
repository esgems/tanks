using UnityEngine;

public static class BallisticMath
{
	private static float m_gravity = Physics.gravity.y;

	/// <summary>
	/// Возвращает угол баллистической траектории снаряда
	/// </summary>
	public static bool TryGetVerticalAngle(Vector3 startPos, Vector3 targetPos, float velocity, bool lowAngle, out float angle)
	{
		var taregtDirection = targetPos - startPos;
		var distance = taregtDirection.magnitude;
		var velocitySquare = velocity * velocity;
		var velocityFactor = Mathf.Sqrt(velocitySquare * velocitySquare - m_gravity * (m_gravity * (distance * distance) - (2 * taregtDirection.y * velocitySquare)));

		if (!double.IsNaN(velocityFactor))
		{
			if (lowAngle)
			{
				velocityFactor = -velocityFactor;
			}

			angle = (velocitySquare + velocityFactor) / (m_gravity * distance);
			angle = -Mathf.Atan(angle) * Mathf.Rad2Deg;

			return true;
		}

		angle = 0;

		return false;
	}

	/// <summary>
	/// Возвращает Позицию в баллистической траектории
	/// </summary>
	public static Vector3 GetPosition(Vector3 direction, float velocity, float time)
	{
		var pos = direction * (velocity * time);
		pos.y += m_gravity * 0.5f * (time * time);

		return pos;
	}

	/// <summary>
	/// Угол между 2 векторами в градусах
	/// </summary>
	public static float GetAngle(Vector3 direction1, Vector3 direction2)
	{
		var angleCos = Vector3.Dot(direction1, direction2);

		return Mathf.Acos(Mathf.Clamp(angleCos, -1f, 1f)) * Mathf.Rad2Deg;
	}
}
