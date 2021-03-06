﻿using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
	public TowerSelector towerSelector;
	public TowerStats stats;

	public bool active;

	void Start()
	{
		towerSelector = GetComponentInParent<TowerSelector>();
	}

	public void SetStats(TowerStats stats, int index)
	{
		this.stats = stats;

		//NOTE(Simon): Index [1], to ignore self.
		GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(stats.image);
		GetComponentInChildren<Text>().text = index.ToString();
	}

	public void SetTowerPreview()
	{
		if (!active)
		{
			towerSelector.ResetButtons();
			World.SetTowerPreview(stats);
			GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("Textures/cancel");
			active = true;
		}
		else
		{
			CancelTowerPreview();
		}
	}

	public void CancelTowerPreview()
	{
		World.CancelTowerPreview();
		GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(stats.image);
		active = false;
	}

	private void OnMouseEnter()
	{
		towerSelector.toolTip.SetActive(true);
		towerSelector.toolTip.GetComponent<TowerTooltip>().SetStats(stats);
	}

	private void OnMouseOver()
	{
		var pos = Input.mousePosition;
		pos.y += 20;
		towerSelector.toolTip.transform.position = pos;
	}

	private void OnMouseExit()
	{
		towerSelector.toolTip.SetActive(false);
	}
}
