using System;
using UnityEngine;

[System.Serializable]
public class TileConfiguration
{
	public Vector2 position;
	public int personalityIndex;

	public TileConfiguration (Vector2 position, int personalityIndex)
	{
		this.position = position;
		this.personalityIndex = personalityIndex;
	}
}


