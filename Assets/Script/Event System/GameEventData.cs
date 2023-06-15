using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Event Data")]
public class GameEventData : ScriptableObject
{
    public List <GameEventListenerWithData> listenerList = new List<GameEventListenerWithData>();

    // Call event though different methods signatures
    public void Call(Component sender, object data)
    {
        for(int i = 0; i < listenerList.Count; i++)
        {
            listenerList[i].OnEventCall(sender,data);
        }
    }

    // Manage Listeners
    public void RegisterListener(GameEventListenerWithData listener)
    {
        if (!listenerList.Contains(listener))
        {
            listenerList.Add(listener);
        }
    }

    public void UnregisterListener(GameEventListenerWithData listener)
    {
        if (listenerList.Contains(listener))
        {
            listenerList.Remove(listener);
        }
    }
}
