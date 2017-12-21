using System;
using UnityEngine;
using UnityEngine.UI;

public class TowerTooltip : MonoBehaviour
{
	public GameObject costValue;
	public GameObject damageValue;
	public GameObject firerateValue;
	public GameObject rangeValue;

	public void SetStats(TowerStats stats)
	{
		costValue.GetComponent<Text>().text = "$ " + stats.cost;
		damageValue.GetComponent<Text>().text = stats.damage.ToString();
		firerateValue.GetComponent<Text>().text = Math.Round(1 / stats.shootInterval, 2) + "/s";
		rangeValue.GetComponent<Text>().text = stats.range.ToString();
	}
}
