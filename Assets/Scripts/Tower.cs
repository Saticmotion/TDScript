using UnityEngine;

public class Tower : MonoBehaviour
{
	public GameObject rangeIndicator;

	public float shootInterval;
	public float timeSinceLastShot;
	public int damage;
	public int range;
	public bool showRangeThisFrame;
	public bool active;

	private LineRenderer lazor;

	void Start()
	{
		lazor = GetComponent<LineRenderer>();
	}

	void Update()
	{
		timeSinceLastShot += Time.deltaTime;

		var monsters = World.monsters;

		if (timeSinceLastShot > shootInterval && active)
		{
			if (monsters.Count > 0)
			{
				GameObject nearest = monsters[0];
				float nearestDistance = float.MaxValue;

				foreach (var monstar in monsters)
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
					lazor.enabled = true;
					lazor.SetPositions(new[] { transform.position, nearest.transform.position });
					nearest.GetComponent<Monstar>().Hit(damage);

					timeSinceLastShot = 0;
				}
			}
		}
		else
		{
			lazor.enabled = false;
		}

		if (showRangeThisFrame)
		{
			rangeIndicator.SetActive(true);
			showRangeThisFrame = false;
		}
		else
		{
			rangeIndicator.SetActive(false);
		}
	}

	public void ShowRange()
	{
		showRangeThisFrame = true;
	}

	public void SetStats(TowerStats stats)
	{
		damage = stats.damage;
		range = stats.range;
		shootInterval = stats.shootInterval;
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(stats.image);
		rangeIndicator.transform.localScale = new Vector3(range * 2, range * 2);
	}
}

public struct TowerStats
{
	public int damage;
	public int range;
	public float shootInterval;
	public int cost;
	public string image;
}

public class TowerTypes
{
	public static TowerStats regular = new TowerStats
	{
		damage = 10,
		range = 3,
		shootInterval = 0.5f,
		cost = 5,
		image = "Textures/tower"
	};
	
	public static TowerStats stronk = new TowerStats
	{
		damage = 25,
		range = 4,
		shootInterval = 1.5f,
		cost = 10,
		image = "Textures/towerRed"
	};
}
