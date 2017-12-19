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

			if (Input.GetMouseButtonDown(0) 
				&& World.map[pos.x, pos.y] == null
				&& World.money >= 10)
			{
				PlaceTower(pos);
				World.money -= 10;
			}

			towerPreview.transform.position = World.LocalToWorldPos(pos);
			towerPreview.GetComponent<Tower>().ShowRange();

			if (Input.GetKey(KeyCode.Escape))
			{
				mode = Mode.None;
				towerPreview.transform.position = new Vector3(-100, -100);
			}
		}
	}

	void PlaceTower(Vector2Int localPos)
	{
		var tower = Instantiate(towerPrefab, World.LocalToWorldPos(localPos), Quaternion.identity);
		tower.GetComponent<Tower>().active = true;
		World.map[localPos.x, localPos.y] = tower;
	}
}
