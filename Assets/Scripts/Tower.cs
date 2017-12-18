using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public Spawner spawner;
	public GameObject rangeIndicator;

	public float shootInterval = 0.5f;
	public float timeSinceLastShot;
	public int damage = 1;
	public int range;

	private LineRenderer lazor;

	void Start()
	{
		spawner = FindObjectOfType<Spawner>();
		lazor = GetComponent<LineRenderer>();
		range = 3;
		rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2);
	}

	void Update()
	{
		timeSinceLastShot += Time.deltaTime;

		var monstars = spawner.monstars;

		if (timeSinceLastShot > shootInterval)
		{
			if (monstars.Count > 0)
			{
				GameObject nearest = monstars[0];
				float nearestDistance = float.MaxValue;

				foreach (var monstar in monstars)
				{
					var distance = Vector2.Distance(transform.position, monstar.transform.position);
					if (distance < nearestDistance)
					{
						nearest = monstar;
						nearestDistance = distance;
					}
				}

				if (nearestDistance < World.LocalToWorldDist(range))
				{
					lazor.SetPositions(new[] { transform.position, nearest.transform.position });
					nearest.GetComponent<Monstar>().Hit(damage);

					timeSinceLastShot = 0;
				}
			}
		}
		else
		{
			lazor.SetPositions(new[] { transform.position, transform.position });
		}
	}
}
