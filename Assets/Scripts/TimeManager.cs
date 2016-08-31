using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public delegate void ReversingTimeAction();

class ActionWithTime
{
    public ActionWithTime(float time, ReversingTimeAction action)
    {
        this.time = time;
        this.actionToCarry = action;
    }
    public float time;
    public ReversingTimeAction actionToCarry;
}

public class TimeManager
{
    private float time;

    public TimeManager()
    {
        TimeDependants = new List<ITimeChanging>();
    }
    
    public float Time
    {
        get
        {
            return time;
        }
        set
        {
            if (value != time)
            {
                var delta = value - time;
                if (delta < 0)
                {
                    while ((actions.Count > 0) && (actions.Peek().time >= time + delta))
                    {
                        var nowDelta = actions.Peek().time - time;
                        applyTimechange(nowDelta);
                        var action = actions.Pop();
                        action.actionToCarry();
                        applyTimechange(nowDelta);
                        delta -= nowDelta;
                    }
                    applyTimechange(delta);
                }
                else
                {
                    applyTimechange(delta);                    
                }
            }
            
        }
    }

    private void applyTimechange(float delta)
    {
        foreach (var timeDep in TimeDependants)
        {
            timeDep.AddTime(delta);
        }
        time += delta;
    }

    public void SetTimeBruteForce(float time)
    {
        this.time = time;
    }
    
    public IEnumerable<ITimeChanging> TimeDependants { get; set; }

    public void RememberAction(ReversingTimeAction action)
    {
        actions.Push(new ActionWithTime(Time, action));
    }

    Stack<ActionWithTime> actions = new Stack<ActionWithTime>();

}

