using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
	public enum Mode
	{
		Placing,
		None
	}
	
	public Mode mode;
	public GameObject towerPrefab;
	public GameObject towerPreview;

	void Start()
	{
		mode = Mode.None;
		towerPreview = Instantiate(towerPrefab, new Vector3(-100, -100), Quaternion.identity);
	}

	void Update()
	{
		if (mode == Mode.None)
		{
			if (Input.GetKey(KeyCode.F1))
			{
				mode = Mode.Placing;
			}
		}

		if (mode == Mode.Placing)
		{
			var pos = World.WorldToLocalPos(Input.mousePosition);

			if (Input.GetMouseButtonDown(0))
			{
				PlaceTower(pos);
			}

			towerPreview.transform.position = World.LocalToWorldPos(pos);

			if (Input.GetKey(KeyCode.Escape))
			{
				mode = Mode.None;
				towerPreview.transform.position = new Vector3(-100, -100);
			}
		}
	}

	void PlaceTower(Vector3 localPos)
	{
		var tower = Instantiate(towerPrefab, World.LocalToWorldPos(localPos), Quaternion.identity);
	}
}
