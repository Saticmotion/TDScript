using UnityEngine;
using System;

public class Monstar : MonoBehaviour
{
	public GameObject prevPoint;
	public GameObject nextPoint;

	public int pathIndex;
	public bool dead;
	public int maxHp;
	public int hp;
	public int reward;

	void Start()
	{
		prevPoint = World.path[0];
		nextPoint = World.path[1];

		maxHp = 2;
		hp = maxHp;
		reward = maxHp;
	}

	void Update()
	{
		if ((transform.position - nextPoint.transform.position).magnitude < 5f)
		{
			if (pathIndex < World.path.Count - 1)
			{
				pathIndex++;
				prevPoint = nextPoint;
				nextPoint = World.path[pathIndex];
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
