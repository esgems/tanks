using UnityEngine;

namespace BaseUI
{
	public class UIManager : SingletonComponent<UIManager>
	{
		private const string UIPathFormat = "Prefabs/UI/{0}";

		public void Create<T>()
		{
			var uiName = typeof(T).Name.Replace("Context", "");
			var ui = Resources.Load(string.Format(UIPathFormat, uiName));

			if (ui != null)
			{
				Instantiate(ui, transform);
			}
		}
	}
}