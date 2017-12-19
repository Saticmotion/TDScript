using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public static List<GameObject> monsters;
	public static List<GameObject> path;
	public static GameObject[,] map;
	
	public static int money;

	public GameObject pathHolder;
	public GameObject pathPointPrefab;

	public void Start()
	{
		monsters = new List<GameObject>();
		path = new List<GameObject>();

		var widthTiles = WorldToLocalDist(Screen.width);
		var heightTiles = WorldToLocalDist(Screen.height);
		map = new GameObject[widthTiles, heightTiles];

		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(0, 10)), Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(10, 10)), Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(10, 0)), Quaternion.identity, pathHolder.transform));
		path.Add(Instantiate(pathPointPrefab, LocalToWorldPos(new Vector2Int(0, 0)), Quaternion.identity, pathHolder.transform));

		for (int i = 0; i < path.Count; i++)
		{
			path[i].name = "point" + i;
		}

		money = 10;
	}

	public void Update()
	{
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

		var mousePos = WorldToLocalPos(Input.mousePosition);
		if (LocalPosValid(mousePos) && map[mousePos.x, mousePos.y] != null)
		{
			map[mousePos.x, mousePos.y].GetComponent<Tower>().ShowRange();
		}
	}

	public void OnGUI()
	{
		GUI.Label(new Rect(0, 0, 100, 20), "money: " + money);
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
