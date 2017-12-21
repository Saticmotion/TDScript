using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
	public GameObject TowerButtonPrefab;
	public GameObject toolTip;
	public GameObject expandButton;
	public List<GameObject> towerButtons; 

	public bool expanded;
	public Vector3 startPos;
	public Vector3 collapsedPos;

	public void Start()
	{
		var towerButton = Instantiate(TowerButtonPrefab, transform, false);
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.regular);
		towerButtons.Add(towerButton);

		towerButton = Instantiate(TowerButtonPrefab, transform, false);
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.stronk);
		towerButtons.Add(towerButton);

		expanded = true;
		startPos = transform.position;
		collapsedPos = startPos;
		collapsedPos.y -= 100f;
	}

	public void ResetButtons()
	{
		foreach (var button in towerButtons)
		{
			//NOTE(Simon): Cancel all active previews. Easy way to switch between towers without accidentally activating two.
			button.GetComponent<TowerButton>().CancelTowerPreview();
		}
	}

	public void Expand()
	{
		if (expanded)
		{
			transform.position = collapsedPos;
			expandButton.GetComponentInChildren<Text>().text = "^";
			expanded = false;
		}
		else
		{
			transform.position = startPos;
			expandButton.GetComponentInChildren<Text>().text = "v";
			expanded = true;
		}
	}
}
