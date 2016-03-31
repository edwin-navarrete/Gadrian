using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class IntEvent : UnityEvent<int>
{
}

public class EventManager : MonoBehaviour
{

    private Dictionary<Events, UnityEventBase> eventDictionary;

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
            eventDictionary = new Dictionary<Events, UnityEventBase>();
        }
    }

    public static void StartListening(Events eventToListen, UnityAction<int> listener)
    {
        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventToListen, out thisEvent))
        {
            if (thisEvent is IntEvent)
            {
                ((IntEvent)thisEvent).AddListener(listener);
            }
            else
            {
                Debug.LogError("Unable to start listening:" + eventToListen);
            }
        }
        else
        {
            IntEvent ev = new IntEvent();
            ev.AddListener(listener);
            instance.eventDictionary.Add(eventToListen, ev);
        }
    }

    public static void StartListening (Events eventToListen, UnityAction listener )
    { 
        UnityEventBase thisEvent = null;
        if ( instance.eventDictionary.TryGetValue( eventToListen, out thisEvent ) )
        {
            if(thisEvent is UnityEvent)
            {
                ((UnityEvent)thisEvent).AddListener(listener);
            }
            else
            {
                Debug.LogError("Unable to start listening:"+eventToListen);
            }            
        }
        else
        {
            UnityEvent ev = new UnityEvent();
            ev.AddListener( listener );
            instance.eventDictionary.Add( eventToListen, ev );
        }
    }

    public static void StopListening(Events eventToStopListening, UnityAction<int> listener)
    {
        if (eventManager == null) return;

        UnityEventBase thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventToStopListening, out thisEvent))
        {
            if (thisEvent is IntEvent)
            {
                ((IntEvent)thisEvent).RemoveListener(listener);
            }
            else
            {
                Debug.LogError("Unable to remove listener for:" + eventToStopListening);
            }
        }
    }

    public static void StopListening (Events eventToStopListening, UnityAction listener)
    {
        if ( eventManager == null ) return;

        UnityEventBase thisEvent = null;
        if ( instance.eventDictionary.TryGetValue( eventToStopListening, out thisEvent ) )
        {
            if (thisEvent is UnityEvent)
            {
                ((UnityEvent)thisEvent).RemoveListener(listener);
            }
            else
            {
                Debug.LogError("Unable to remove listener for:" + eventToStopListening);
            }
        }
    }

    public static void TriggerEvent (Events eventToTrigger, int value = -1)
    {
        UnityEventBase thisEvent;
        if ( instance.eventDictionary.TryGetValue( eventToTrigger, out thisEvent ) )
        {
            if (thisEvent is UnityEvent)
            {
                ((UnityEvent)thisEvent).Invoke();
            }
            else if (thisEvent is UnityEvent<int>)
            {
                ((IntEvent)thisEvent).Invoke(value);
            }
        }
    }
}
