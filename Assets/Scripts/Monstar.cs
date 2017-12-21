using UnityEngine;
using System;

public class Monstar : MonoBehaviour
{
	public GameObject prevPoint;
	public GameObject nextPoint;
	public GameObject maxHpSprite;
	public GameObject curHpSprite;

	public int pathIndex;
	public bool dead;
	public int maxHp;
	public int hp;
	public int reward;

	public float maxHpSpriteWidth;

	void Start()
	{
		prevPoint = World.path[0];
		nextPoint = World.path[1];
		maxHpSpriteWidth = maxHpSprite.transform.localScale.x;
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

		var curScale = curHpSprite.transform.localScale;
		var curPos = curHpSprite.transform.localPosition;
		var fraction = (hp / (float)maxHp);

		//NOTE(Simon): Scale healthbar size
		curScale.x = maxHpSpriteWidth * fraction;
		curHpSprite.transform.localScale = curScale;

		//NOTE(Simon): Offset scaled healthbar
		curPos.x = -maxHpSpriteWidth / 2 * (1 - fraction);
		curHpSprite.transform.localPosition = curPos;
	}

	public void SetStats(int reward, int maxHp)
	{
		this.maxHp = maxHp;
		this.hp = maxHp;
		this.reward = reward;
	}

	public void Hit(int damage)
	{
		if (damage > maxHp)
		{
			dead = true;
		}

		hp = Mathf.Max(0, hp - damage);

		if (hp <= 0)
		{
			dead = true;
		}
	}
}
