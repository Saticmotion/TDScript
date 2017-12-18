using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	public List<GameObject> points;

	void Start()
	{
		points[0].transform.position = World.LocalToWorldPos(new Vector3(0, 10));
		points[1].transform.position = World.LocalToWorldPos(new Vector3(10, 10));
		points[2].transform.position = World.LocalToWorldPos(new Vector3(10, 0));
		points[3].transform.position = World.LocalToWorldPos(new Vector3(0, 0));
	}

	void Update()
	{
		for (int i = 0; i < Screen.width / World.unitSize; i++)
		{
			Debug.DrawLine(new Vector3(i * World.unitSize, 0), new Vector3(i * World.unitSize, Screen.height));
		}

		for (int i = 0; i < Screen.height / World.unitSize; i++)
		{
			Debug.DrawLine(new Vector3(0, i * World.unitSize), new Vector3(Screen.width, i * World.unitSize));
		}
	}
}