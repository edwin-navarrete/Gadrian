using UnityEngine;
using System.Collections.Generic;

public class NeighbourSensor : MonoBehaviour
{
	private new Collider2D collider;
	private List<Personality> neighbours;
	public List<Personality> Neighbours
	{
		get
		{
			return neighbours;
		}
	}

	public event UnityEngine.Events.UnityAction SensorAlert;

	public void Awake ()
	{
		collider = GetComponent<Collider2D> ();
	}

	public void Start ()
	{
		neighbours = new List<Personality> ();
	}

	public void OnTriggerEnter2D (Collider2D other)
	{
		if ( other.transform.tag == "Player" )
		{
			Personality personality = other.GetComponent<Personality> ();
			neighbours.Add ( personality );
			personality.RefreshMood ();
		}
	}

	public void OnTriggerExit2D (Collider2D other)
	{
		if ( other.transform.tag == "Player" )
		{
			Personality personality = other.GetComponent<Personality> ();
			neighbours.Remove ( personality );
			personality.RefreshMood ();
		}
	}

	
}
