using UnityEngine;
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

	public void SetStats(TowerStats stats)
	{
		this.stats = stats;

		//NOTE(Simon): Index [1], to ignore self.
		GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(stats.image);
	}

	public void SetTowerPreview()
	{
		//TODO(Simon): Currently you can click on one tower button, and then another. The first's state won't update
		if (!active)
		{
			World.SetTowerPreview(stats);
			GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>("Textures/cancel");
			active = true;
		}
		else
		{
			World.CancelTowerPreview();
			GetComponentsInChildren<Image>()[1].sprite = Resources.Load<Sprite>(stats.image);
			active = false;
		}
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
