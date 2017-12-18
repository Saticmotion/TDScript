using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject monstarPrefab;
	public List<GameObject> monstars;

	public Path path;
	public GameObject startPoint;

	public float spawnInterval = 2;
	public float timeSinceLastSpawn;

	void Start()
	{
		path = FindObjectOfType<Path>();
		startPoint = path.points[0];
	}

	void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > spawnInterval)
		{
			var monstar = Instantiate(monstarPrefab, startPoint.transform.position, Quaternion.identity);
			monstars.Add(monstar);
			timeSinceLastSpawn = 0;
		}

		for (int i = monstars.Count - 1; i >= 0; i--)
		{
			if (monstars[i].GetComponent<Monstar>().dead)
			{
				Destroy(monstars[i]);
				monstars.RemoveAt(i);
			}
		}
	}
}
