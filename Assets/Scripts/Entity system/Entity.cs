using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    public abstract int Health { get;  set;}
    public abstract int Hunger { get;  set; }
    public abstract float Speed { get; protected set; }
    public abstract float RunSpeed { get; protected set; }
    public abstract World world { get; set; }
    public abstract WorldTime WTime { get; set; }

    public virtual void Sleep()
    {
        if(WTime.UniversalTime == TimeType.Night)
        {
            WTime.SkipNight();
        }
    }

    public virtual void Init()
    {
        world = World.Instance;
        WTime = WorldTime.Instance;
    }

}
