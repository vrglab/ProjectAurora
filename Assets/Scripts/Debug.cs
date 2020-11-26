using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectAurora.Diagnostics
{
    
    class Debug
    {
        
        public static string Task { get; protected set; }


        public static void log(object massage)
        {
            System.Diagnostics.Debug.WriteLine(Application.productName + ": " + massage);
        }

        public static void task(object massage)
        {
            Task = massage.ToString();
        }

        public static void logError(object massage)
        {
            UnityEngine.Debug.LogError(Application.productName + ": " + massage);
        }

        public static void logWarning(object massage)
        {
            UnityEngine.Debug.LogWarning(Application.productName + ": " + massage);
        }

        public static void log(object massage, GameObject obj)
        {
            UnityEngine.Debug.Log(Application.productName + ": " + massage,obj);
        }
        public static void logError(object massage, GameObject obj)
        {
            UnityEngine.Debug.LogError(Application.productName + ": " + massage, obj);
        }

        public static void logWarning(object massage, GameObject obj)
        {
            UnityEngine.Debug.LogWarning(Application.productName + ": " + massage,obj);
        }

    }

    class LoadingTask
    {

        public Text loadingTaskText;

        public string task { get; protected set; }


        public LoadingTask (Text taskText, string crTask)
        {
            loadingTaskText = taskText;
            task = crTask;
        }

        public LoadingTask(string crTask)
        {
            loadingTaskText = null;
            task = crTask;
        }

        public LoadingTask()
        {

        }

        public void SetTaskText(Text newTaskText)
        {


            loadingTaskText = newTaskText;

        }

        public void SetTask(string newTask)
        {

            task = newTask;

            loadingTaskText.text = task;

        }

    }

}
