using UnityEngine;
using UnityEngine.UI;

public class LevelDetails : MonoBehaviour
{
	public Text level;
	public Text money;
	public Text monsterHp;
	public Text monstersLeft;

	void Update()
	{
		level.text = World.level.ToString();
		money.text = World.money.ToString();
		monsterHp.text = World.monsterHp.ToString();
		monstersLeft.text = World.monstersLeft.ToString();
	}
}
