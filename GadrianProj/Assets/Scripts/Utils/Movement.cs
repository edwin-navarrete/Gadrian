using UnityEngine;
using System;

public class Movement
{
	private readonly GameObject sender;
	private readonly MovementAction action;
	private readonly Vector3 oldPosition;
	private readonly Vector3 newPosition;

	public GameObject Sender
	{
		get
		{
			return sender;
		}
	}

	public MovementAction ActionPerformed
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

	public Movement (GameObject sender, MovementAction action, Vector3 oldPosition, Vector3 newPosition)
	{
		this.sender = sender;
		this.action = action;
		this.oldPosition = oldPosition;
		this.newPosition = newPosition;
	}
}

public enum MovementAction
{
	Placement,
	Movement
}

