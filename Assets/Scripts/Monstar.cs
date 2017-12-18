using UnityEngine;
using System;

public class Monstar : MonoBehaviour
{
	public GameObject prevPoint;
	public GameObject nextPoint;
	public Path path;

	public int index;
	public bool dead;
	public int maxHp;
	public int hp;

	void Start()
	{
		path = FindObjectOfType<Path>();
		prevPoint = path.points[0];
		nextPoint = path.points[1];

		maxHp = 2;
		hp = maxHp;
	}

	void Update()
	{
		if ((transform.position - nextPoint.transform.position).magnitude < 5f)
		{
			if (index < path.points.Count - 1)
			{
				index++;
				prevPoint = nextPoint;
				nextPoint = path.points[index];
			}
			else
			{
				dead = true;
			}
		}

		var direction = Vector3.Normalize(nextPoint.transform.position - prevPoint.transform.position);
		transform.position += direction * World.LocalToWorldDist(1) * Time.deltaTime;
	}

	public void Hit(int damage)
	{
		if (damage > maxHp)
		{
			dead = true;
		}

		hp -= damage;

		if (hp <= 0)
		{
			dead = true;
		}
	}
}
