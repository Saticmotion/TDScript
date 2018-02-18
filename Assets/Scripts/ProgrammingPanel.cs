using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgrammingPanel : MonoBehaviour
{
	public RectTransform content;
	public List<GameObject> items;

	public GameObject ProgramStepPrefab;

	void Start()
	{
		items = new List<GameObject>();
	}

	void Update()
	{
		bool dirty = false;

		if (Input.GetMouseButtonUp(0) && RectTransformUtility.RectangleContainsScreenPoint(content, Input.mousePosition))
		{
			var newItem = Instantiate(ProgramStepPrefab, content);
			Vector2 mousePos;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(content, Input.mousePosition, Camera.main, out mousePos);
			newItem.GetComponent<RectTransform>().localPosition = mousePos;
			items.Add(newItem);
			dirty = true;
		}

		if (dirty)
		{
			float minRight = content.rect.xMax;
			float minBottom = content.rect.yMax;
			float right = 0;
			float bottom = 0;

			foreach (var item in items)
			{
				Vector3[] corners = new Vector3[4];
				var corner = item.GetComponent<RectTransform>().localPosition;

				Debug.Log(corner.x + " " + corner.y);
				if (corner.x > right)
				{
					right = corner.x;
				}
				if (corner.y < bottom)
				{
					bottom = corner.y;
				}
			}

			content.sizeDelta = new Vector2(Mathf.Max(minRight, right + 100), Mathf.Abs(Mathf.Min(minBottom, bottom - 100)));
			//Debug.Log(content.rect.max);
		}
	}
}
