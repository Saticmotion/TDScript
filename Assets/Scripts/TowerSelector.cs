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
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.regular, towerButtons.Count + 1);
		towerButtons.Add(towerButton);

		towerButton = Instantiate(TowerButtonPrefab, transform, false);
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.stronk, towerButtons.Count + 1);
		towerButtons.Add(towerButton);

		expanded = true;
		startPos = transform.position;
		collapsedPos = startPos;
		collapsedPos.y -= 100f;
	}

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) { if (towerButtons.Count >= 1) { towerButtons[0].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) { if (towerButtons.Count >= 2) { towerButtons[1].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3)) { if (towerButtons.Count >= 3) { towerButtons[2].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4)) { if (towerButtons.Count >= 4) { towerButtons[3].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5)) { if (towerButtons.Count >= 5) { towerButtons[4].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6)) { if (towerButtons.Count >= 6) { towerButtons[5].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7)) { if (towerButtons.Count >= 7) { towerButtons[6].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8)) { if (towerButtons.Count >= 8) { towerButtons[7].GetComponent<TowerButton>().SetTowerPreview(); } }
		if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9)) { if (towerButtons.Count >= 9) { towerButtons[8].GetComponent<TowerButton>().SetTowerPreview(); } }
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
