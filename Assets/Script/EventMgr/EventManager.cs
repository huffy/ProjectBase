using UnityEngine;
using System.Collections;
using System;
using Assets.Script.Base;
using UnityEngine.Events;
using System.Collections.Generic;

public enum EventDefine
{
    TiggerType,      //触发开关
    ChangeScene,     //切换场景
}


public class EventManager : TSingleton<EventManager>, IDisposable
{
    private Dictionary<EventDefine, UnityAction<object, EventArgs>> mEventDic = new Dictionary<EventDefine, UnityAction<object, EventArgs>>();

    public EventManager()
    {
        if (mEventDic == null)
        {
            mEventDic = new Dictionary<EventDefine, UnityAction<object, EventArgs>>();
            mEventDic.Clear();
        }
    }

    public void AddListener(EventDefine eventID, UnityAction<object, EventArgs> eventHadle)
    {
        if (!mEventDic.ContainsKey(eventID))
        {
            mEventDic.Add(eventID, eventHadle);
        }
        else
        {
            mEventDic[eventID] += eventHadle;
        }
    }

    public void RemoveListener(EventDefine eventID, UnityAction<object, EventArgs> eventHadle)
    {
        if (mEventDic.ContainsKey(eventID))
        {
            mEventDic[eventID] -= eventHadle;
        }
    }


    public void RasieEvent(EventDefine eventID, object obj, EventArgs e)
    {
        if (mEventDic.ContainsKey(eventID))
        {
            mEventDic[eventID].Invoke(obj, e);
        }
    }

    public void RemoveAllListener()
    {
        if (mEventDic != null)
        {
            mEventDic.Clear();
        }
    }

    public override void Dispose()
    {
        base.Dispose();
        RemoveAllListener();
        mEventDic = null;
    }



}
