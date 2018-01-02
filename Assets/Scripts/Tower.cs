using UnityEngine;

public class Tower : MonoBehaviour
{
	public GameObject rangeIndicator;

	public int level;
	public TowerStats stats;

	public float timeSinceLastShot;
	public bool showRangeThisFrame;
	public bool active;

	private LineRenderer lazor;

	void Start()
	{
		lazor = GetComponent<LineRenderer>();
		level = 1;
	}

	void Update()
	{
		timeSinceLastShot += Time.deltaTime;

		var monsters = World.monsters;

		if (timeSinceLastShot > stats.shootInterval && active)
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

				if (nearestDistance < World.LocalToWorldDist(stats.range))
				{
					lazor.enabled = true;
					lazor.SetPositions(new[] { transform.position, nearest.transform.position });
					nearest.GetComponent<Monstar>().Hit(stats.damage);

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

	public void ShowDetails()
	{
		World.towerDetails.SetActive(true);
		World.towerDetails.GetComponent<TowerDetails>().SetStats(stats, this);
		var pos = transform.position;
		pos.x += 20;
		pos.y -= 20;
		World.towerDetails.transform.position = pos;
	}

	public void SetStats(TowerStats stats)
	{
		stats.range += 0.5f;
		this.stats = stats;
		GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(stats.image);
		var rangeSpriteSize = rangeIndicator.GetComponent<SpriteRenderer>().sprite.rect;
		var rangeSizeLocal = World.WorldToLocalDist(rangeSpriteSize.width);
		rangeIndicator.transform.localScale = new Vector3(stats.range / rangeSizeLocal * 2, stats.range / rangeSizeLocal * 2);
	}
}

public struct TowerStats
{
	public int damage;
	public float range;
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
