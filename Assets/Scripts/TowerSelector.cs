using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelector : MonoBehaviour
{
	public void SetRegular()
	{
		World.SetTowerToPlace(TowerTypes.regular);
	}

	public void SetStronk()
	{
		World.SetTowerToPlace(TowerTypes.stronk);
	}
}
