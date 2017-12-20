using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public enum Mode
	{
		PlacingTower,
		None
	}

	public static List<GameObject> monsters;
	public static List<GameObject> path;
	public static GameObject[,] map;

	public static int money;
	public static int level;

	public GameObject pathHolder;
	public GameObject pathPointPrefab;
	public GameObject monstarPrefab;
	public GameObject towerPrefab;
	public GameObject towerPreview;

	public Mode mode;
	public float spawnInterval;
	public float timeSinceLastSpawn;
	public int monstersLeft;

	public void Start()
	{
		monsters = new List<GameObject>();
		path = new List<GameObject>();
		
		var widthTiles = WorldToLocalDist(Screen.width);
		var heightTiles = WorldToLocalDist(Screen.height);
		map = new GameObject[widthTiles, heightTiles];
		
		towerPreview = Instantiate(towerPrefab, new Vector3(-100, -100), Quaternion.identity);

		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(1				, heightTiles - 2))	, Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(widthTiles - 2	, heightTiles - 2))	, Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(widthTiles - 2	, 1))				, Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(1				, 1))				, Quaternion.identity, pathHolder.transform));

		for (int i = 0; i < path.Count; i++)
		{
			path[i].name = "point" + i;
		}

		mode = Mode.None;
		money = 10;
		level = 0;
		spawnInterval = 1;
	}

	public void Update()
	{
		if (mode == Mode.None)
		{
			var mousePos = WorldToLocalPos(Input.mousePosition);
			if (LocalPosValid(mousePos) && map[mousePos.x, mousePos.y] != null)
			{
				map[mousePos.x, mousePos.y].GetComponent<Tower>().ShowRange();
			}

			if (Input.GetKey(KeyCode.F1))
			{
				mode = Mode.PlacingTower;
			}
		}

		if (mode == Mode.PlacingTower)
		{
			var pos = World.WorldToLocalPos(Input.mousePosition);

			if (Input.GetMouseButtonDown(0)
				&& map[pos.x, pos.y] == null
				&& money >= 10)
			{
				PlaceTower(pos);
				money -= 10;
			}

			towerPreview.transform.position = LocalToWorldPos(pos);
			towerPreview.GetComponent<Tower>().ShowRange();

			if (Input.GetKey(KeyCode.Escape))
			{
				mode = Mode.None;
				towerPreview.transform.position = new Vector3(-100, -100);
			}
		}

		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > spawnInterval)
		{
			var monster = Instantiate(monstarPrefab, path[0].transform.position, Quaternion.identity);
			monster.GetComponent<Monstar>().SetStats((int)(level * 1.5f), (int)(10 * Mathf.Pow(1.25f, level - 1)));
			monsters.Add(monster);
			timeSinceLastSpawn = 0;
			monstersLeft--;

			if (monstersLeft <= 0)
			{
				level += 1;
				monstersLeft = 10;
			}
		}
		for (int i = monsters.Count - 1; i >= 0; i--)
		{
			var m = monsters[i].GetComponent<Monstar>();
			if (m.dead)
			{
				money += m.reward;
				Destroy(monsters[i]);
				monsters.RemoveAt(i);
			}
		}
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 20), "money: " + money);
		GUI.Label(new Rect(0, 20, 100, 20), "level: " + level);
		GUI.Label(new Rect(0, 40, 100, 20), "hp: " + (int)(10 * Mathf.Pow(1.25f, level - 1)));
		GUI.Label(new Rect(0, 60, 100, 20), "monsters left: " + monstersLeft);
	}

	void PlaceTower(Vector2Int localPos)
	{
		var tower = Instantiate(towerPrefab, LocalToWorldPos(localPos), Quaternion.identity);
		tower.GetComponent<Tower>().active = true;
		map[localPos.x, localPos.y] = tower;
	}

	#region helpers

	public static int unitSize = 40;
	public static int unitOffset = unitSize / 2;

	public static int LocalToWorldPos(int localPos)
	{
		return localPos * unitSize + unitOffset;
	}

	public static int LocalToWorldDist(int localDist)
	{
		return localDist * unitSize;
	}

	
	public static Vector2 LocalToWorldPos(Vector2Int localPos)
	{
		return new Vector2
		{
			x = localPos.x * unitSize + unitOffset,
			y = localPos.y * unitSize + unitOffset
		};
	}

	public static Vector2 LocalToWorldDist(Vector3 localDist)
	{
		return new Vector2
		{
			x = localDist.x * unitSize,
			y = localDist.y * unitSize
		};
	}


	public static int WorldToLocalPos(int worldPos)
	{
		return worldPos / unitSize;
	}

	public static int WorldToLocalDist(int worldDist)
	{
		return worldDist / unitSize;
	}


	public static Vector2Int WorldToLocalPos(Vector3 worldPos)
	{
		return new Vector2Int
		{
			x = (int)(worldPos.x / unitSize),
			y = (int)(worldPos.y / unitSize)
		};
	}

	public static Vector2 WorldToLocalDist(Vector3 worldDist)
	{
		return new Vector2
		{
			x = worldDist.x / unitSize,
			y = worldDist.y / unitSize
		};
	}

	public static bool LocalPosValid(Vector2Int pos)
	{
		return pos.x >= 0 && pos.x < map.GetLength(0)
				&& pos.y >= 0 && pos.y < map.GetLength(1);
	}
	#endregion
}
