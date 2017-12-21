using UnityEngine;
using UnityEngine.UI;

public class TowerSelector : MonoBehaviour
{
	public GameObject TowerButtonPrefab;
	public GameObject toolTip;
	public GameObject expandButton;

	public bool expanded;
	public Vector3 startPos;
	public Vector3 collapsedPos;

	public void Start()
	{
		var towerButton = Instantiate(TowerButtonPrefab, transform, false);
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.regular);
		
		towerButton = Instantiate(TowerButtonPrefab, transform, false);
		towerButton.GetComponent<TowerButton>().SetStats(TowerTypes.stronk);

		expanded = true;
		startPos = transform.position;
		collapsedPos = startPos;
		collapsedPos.y -= 100f;
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
