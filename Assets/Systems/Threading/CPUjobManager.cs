using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine.Bindings;


[Obsolete("This class is none functional")]
public class CPUjobManager : MonoBehaviour
{
    private static NativeArray<JobHandle> jobHandleList = new NativeArray<JobHandle>();


    public static void JobToList(JobHandle job)
    {
        JobHandle jh = job;
     
        
    }


    public static void Complete()
    {
        JobHandle.CompleteAll(jobHandleList);
    }

    public static JobHandle JobSchedualer(Action Job)
    {
        CPUjob cj = new CPUjob(Job);

       return cj.Schedule();

    }



}
[Obsolete("This class is none functional")]
public struct CPUjob : IJob
{

    public Action action;

    public CPUjob(Action act)
    {
        action = act;
    }

    public void Execute()
    {
        action.Invoke();
    }
}
[Obsolete("This class is none functional")]
public struct CPUParraleljob : IJobParallelFor
{

    public Action action;

    public CPUParraleljob(Action act)
    {
        action = act;
    }


    public void Execute(int index)
    {
        action.Invoke();
    }
}

