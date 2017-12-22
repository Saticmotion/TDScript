using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerDetails : MonoBehaviour
{
	public Text damageValue;
	public Text firerateValue;
	public Text rangeValue;

	public void SetStats(TowerStats stats, Tower tower)
	{
		damageValue.text = stats.damage.ToString();
		firerateValue.text = Math.Round(1 / stats.shootInterval, 2) + "/s";
		rangeValue.text = stats.range.ToString(); ;
	}
}
