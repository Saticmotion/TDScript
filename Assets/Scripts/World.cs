using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class World
{
	public static int unitSize = 40;
	public static int unitOffset = unitSize / 2;

	public static int LocalToWorldPos(int localPos)
	{
		return localPos * unitSize + unitOffset;
	}

	public static int LocalToWorldDist(int localDist)
	{
		return localDist * unitSize;
	}

	
	public static Vector3 LocalToWorldPos(Vector3 localPos)
	{
		return new Vector3
		{
			x = localPos.x * unitSize + unitOffset,
			y = localPos.y * unitSize + unitOffset
		};
	}

	public static Vector3 LocalToWorldDist(Vector3 localDist)
	{
		return new Vector3
		{
			x = localDist.x * unitSize,
			y = localDist.y * unitSize
		};
	}


	public static int WorldToLocalPos(int worldPos)
	{
		return worldPos / unitSize;
	}

	public static int WorldToLocalDist(int worldDist)
	{
		return worldDist / unitSize;
	}


	public static Vector3 WorldToLocalPos(Vector3 worldPos)
	{
		return new Vector3
		{
			x = (int)(worldPos.x / unitSize),
			y = (int)(worldPos.y / unitSize)
		};
	}

	public static Vector3 WorldToLocalDist(Vector3 worldDist)
	{
		return new Vector3
		{
			x = worldDist.x / unitSize,
			y = worldDist.y / unitSize
		};
	}

}
