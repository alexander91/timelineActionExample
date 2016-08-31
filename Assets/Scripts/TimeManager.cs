using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public delegate void ReversingTimeActionDelegate();

class ReversingActionWithTime
{
    public ReversingActionWithTime(float time, ReversingTimeActionDelegate action)
    {
        this.time = time;
        this.actionToCarry = action;
    }
    public float time;
    public ReversingTimeActionDelegate actionToCarry;
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

    public void RememberAction(ReversingTimeActionDelegate action)
    {
        actions.Push(new ReversingActionWithTime(Time, action));
    }

    Stack<ReversingActionWithTime> actions = new Stack<ReversingActionWithTime>();

}

