using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{

    private Dictionary<Events, UnityEvent> eventDictionary;

    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if ( !eventManager )
            {
                eventManager = FindObjectOfType( typeof( EventManager ) ) as EventManager;

                if ( !eventManager )
                {
                    GameObject newInstance = new GameObject( "EventManager" );
                    eventManager = newInstance.AddComponent<EventManager>();
                }

                eventManager.Init();
            }

            return eventManager;
        }
    }

    void Init ()
    {
        if ( eventDictionary == null )
        {
            eventDictionary = new Dictionary<Events, UnityEvent>();
        }
    }

    public static void StartListening (Events eventToListen, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if ( instance.eventDictionary.TryGetValue( eventToListen, out thisEvent ) )
        {
            thisEvent.AddListener( listener );
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener( listener );
            instance.eventDictionary.Add( eventToListen, thisEvent );
        }
    }

    public static void StopListening (Events eventToStopListening, UnityAction listener)
    {
        if ( eventManager == null ) return;

        UnityEvent thisEvent = null;
        if ( instance.eventDictionary.TryGetValue( eventToStopListening, out thisEvent ) )
        {
            thisEvent.RemoveListener( listener );
        }
    }

    public static void TriggerEvent (Events eventToTrigger)
    {
        UnityEvent thisEvent;
        if ( instance.eventDictionary.TryGetValue( eventToTrigger, out thisEvent ) )
        {
            thisEvent.Invoke();
        }
    }
}
