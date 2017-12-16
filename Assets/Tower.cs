using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public float shootInterval = 0.5f;
	public float timeSinceLastShot;

	private LineRenderer lazor;

	void Start()
	{
		lazor = GetComponent<LineRenderer>();
	}

	void Update()
	{
		var monstars = Path.instance.monstars;

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

			timeSinceLastShot += Time.deltaTime;

			if (timeSinceLastShot > shootInterval)
			{
				lazor.SetPositions(new[] {transform.position, nearest.transform.position});
				timeSinceLastShot = 0;
			}
			else
			{
				lazor.SetPositions(new[] {transform.position, transform.position});
			}
		}
	}
}
