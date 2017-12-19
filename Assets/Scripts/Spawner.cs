using UnityEngine;

public class Spawner : MonoBehaviour
{
	public GameObject monstarPrefab;

	public GameObject startPoint;

	public float spawnInterval = 2;
	public float timeSinceLastSpawn;

	void Start()
	{
		startPoint = World.path[0];
	}

	void Update()
	{
		timeSinceLastSpawn += Time.deltaTime;
		if (timeSinceLastSpawn > spawnInterval)
		{
			var monstar = Instantiate(monstarPrefab, startPoint.transform.position, Quaternion.identity);
			World.monsters.Add(monstar);
			timeSinceLastSpawn = 0;
		}
	}
}
