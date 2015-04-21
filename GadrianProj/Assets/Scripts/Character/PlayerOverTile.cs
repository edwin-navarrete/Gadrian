using UnityEngine;
using System.Collections.Generic;

public class PlayerOverTile : MonoBehaviour
{
	[SerializeField]
	private Sprite nonSolidTile;
	[SerializeField]
	private Sprite solidTile;

	private new SpriteRenderer renderer;

	public void Awake()
	{
		renderer = GetComponent<SpriteRenderer> ();
	}

	public void SolidifyTile()
	{
		renderer.sprite = solidTile;
	}

	public void UnsolidifyTile()
	{
		renderer.sprite = nonSolidTile;
	}

}