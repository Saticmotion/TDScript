using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
	public static Path instance
	{
		get { return _instance; }
		set { _instance = value; }
	}
	private static Path _instance;

	public List<GameObject> points;

	public GameObject monstarPrefab;
	public List<GameObject> monstars;

	public float spawnInterval = 2;
	public float timeSinceLastSpawn;

	void Start ()
	{
		_instance = this;
	}

	void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > spawnInterval)
		{
			var monstar = Instantiate(monstarPrefab, points[0].transform.position, Quaternion.identity);
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