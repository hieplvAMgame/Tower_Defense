using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCallbackHandle : MonoBehaviour
{
    Dictionary<string, List<Action>> events = new Dictionary<string, List<Action>>();
    public void AddEvent(string eventName, Action action)
    {
        if(!events.ContainsKey(eventName)) events.Add(eventName, new List<Action>());
        events[eventName].Add(action);
    }
    public void InvokeEvent(string name)
    {
        if (events.ContainsKey(name))
            foreach (var _event in events[name])
            {
                _event?.Invoke();
            }
    }
}
