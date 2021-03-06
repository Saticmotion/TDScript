﻿using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
	public Text costValue;
	public Text damageValue;
	public Text firerateValue;
	public Text rangeValue;

	public void SetStats(TowerStats stats)
	{
		costValue.text = "$ " + stats.cost;
		damageValue.text = stats.damage.ToString();
		firerateValue.text = Math.Round(1 / stats.shootInterval, 2) + "/s";
		rangeValue.text = stats.range.ToString();
	}
}
