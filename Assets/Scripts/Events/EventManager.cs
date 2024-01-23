using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public delegate void OnPlayerJumpedEvent();
    public static event OnPlayerJumpedEvent onPlayerJumpedEvent;

    public static void OnPlayerJumpedTrigger()
    {
        onPlayerJumpedEvent?.Invoke();
    }
}

