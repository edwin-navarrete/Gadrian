using UnityEngine;
using System;

public class Movement
{
	private readonly GameObject sender;
	private readonly Action action;
	private readonly Vector3 oldPosition;
	private readonly Vector3 newPosition;

	public GameObject Sender
	{
		get
		{
			return sender;
		}
	}

	public Action ActionPerformed
	{
		get
		{
			return action;
		}
	}

	public Vector3 OldPosition
	{
		get
		{
			return oldPosition;
		}
	}

	public Vector3 NewPosition
	{
		get
		{
			return newPosition;
		}
	}

	public Movement (GameObject sender, Action action, Vector3 oldPosition, Vector3 newPosition)
	{
		this.sender = sender;
		this.action = action;
		this.oldPosition = oldPosition;
		this.newPosition = newPosition;
	}
}

public enum Action
{
	Placement,
	Movement
}

