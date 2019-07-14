using BaseUI;
using UnityEngine;
using UnityEngine.UI;

namespace GameUI
{
	public class AimUIContext : BaseUIContext
	{
		[SerializeField]
		private Color m_normalColor = Color.green;

		[SerializeField]
		private Color m_failColor = Color.red;

		[SerializeField]
		private Image m_image;

		private RectTransform m_canvas;
		private RectTransform m_transform;
		private bool m_availableShot;

		private void Awake()
		{
			m_transform = GetComponent<RectTransform>();

			if (m_image == null)
			{
				m_image = m_transform.GetComponentInChildren<Image>();
			}

			var canvas = GetComponentInParent<Canvas>();

			if (canvas != null)
			{
				m_canvas = canvas.GetComponent<RectTransform>();
			}
		}

		private void Update()
		{
			if (AimSystem.instance.TryGetMousePosition(out var targetPos))
			{
				var viewportPos = Camera.main.WorldToViewportPoint(targetPos);
				var sizeDelta = m_canvas.sizeDelta;
				var screenPos = new Vector2(viewportPos.x * sizeDelta.x - sizeDelta.x * 0.5f, viewportPos.y * sizeDelta.y - sizeDelta.y * 0.5f);

				m_transform.anchoredPosition = screenPos;

				SetAvailable(true);
			}
			else
			{
				SetAvailable(false);
			}
		}

		public void SetAvailable(bool availableToShot)
		{
			if (m_availableShot == availableToShot)
			{
				return;
			}

			m_availableShot = availableToShot;

			if (m_image != null)
			{
				m_image.color = availableToShot ? Color.green : Color.red;
			}
		}
	}
}