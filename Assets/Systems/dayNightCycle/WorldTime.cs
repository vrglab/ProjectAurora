using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using System.Runtime.Serialization;

public class WorldTime : MonoBehaviour
{
    public TimeType UniversalTime { get; protected set; } = TimeType.Day;
    public float PhysicalTime { get; protected set; }
    public float SunLightLevel { get; protected set; }

    public float TimeCycleOfDay = 1200;
    public float TimeCycleOfNight = 1200;

    public float IntensityOfLight;

    public Light2D l;

    public static WorldTime Instance { get; private set; } = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            throw new TwoWorldTimeException();
        }
    }


    private void Update()
    {

            Cycle();
    
        SunLightLevel = l.intensity;
    }

    public void Cycle()
    {
        
        if (UniversalTime == TimeType.Day)
        {
            CycleDay();
        }
        else
        {
            CycleNight();
        }
    }


    public void CycleDay()
    {
        if(TimeCycleOfDay <= 0)
        {
            TimeSet(TimeType.Night);
            TimeCycleOfDay = 1200;
        }
        else
        {
            TimeCycleOfDay -= Time.deltaTime;
            TransitionToNight(IntensityOfLight);
        }
        PhysicalTime = TimeCycleOfDay;
    }
    public void CycleNight()
    {
       
        if (TimeCycleOfNight <= 0)
        {
            TimeSet(TimeType.Day);
            TimeCycleOfNight = 1200;
        }
        else
        {
            TimeCycleOfNight -= Time.deltaTime;
            TransitionToDay(IntensityOfLight);
        }

        PhysicalTime = TimeCycleOfNight;
    }

    public void SkipNight()
    {
        for (int i = (int)IntensityOfLight; i > l.intensity; i++)
        {
            l.intensity += 0.1f;
        }
        TimeSet(TimeType.Day);
        TimeCycleOfNight = 1200;

    }

    public void TransitionToNight(float time)
    {
        for (int i = 0; i < time; i++)
        {
            l.intensity -= 0.000001f;
        }
    }

    public void TransitionToDay(float time)
    {
        for (int i = (int)IntensityOfLight; i > time; i++)
        {
            l.intensity += 0.000001f;
        }
    }

    public void TimeSet(TimeType time)
    {
        UniversalTime = time;
    }
}

public enum TimeType
{
    Day = 0,
    Night = 1
}


public class TwoWorldTimeException : Exception
{
    public TwoWorldTimeException()
    {
        ProjectAurora.Diagnostics.Debug.logError(Message);
        System.Diagnostics.Debug.Fail(Message);

    }

    public TwoWorldTimeException(string message) : base(message)
    {
    }

    public TwoWorldTimeException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected TwoWorldTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public override IDictionary Data => base.Data;

    public override string HelpLink { get => base.HelpLink; set => base.HelpLink = value; }

    public override string Message => "There are two WorldTime scripts";

    public override string Source { get => base.Source; set => base.Source = value; }

    public override string StackTrace => base.StackTrace;

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override Exception GetBaseException()
    {
        return base.GetBaseException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    public override string ToString()
    {
        return base.ToString();
    }
}

