using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.SAL;

public class DirectoryManager : MonoBehaviour
{

    public string SaveType { get; protected set; } = "pa";

    public string datapath { get; set; }
    public static string Savetype { get; protected set; } = "pa";
    public string worldsdataPath { get; protected set; } = "C:/Users/arad8/AppData/LocalLow/vrglab/projekt aurora"+ "/" + "worldsLoad";
    public string worldPath { get; protected set; }
    

    // Start is called before the first frame update
    void Start()
    {

        if(datapath == null)
        {
            datapath = Application.persistentDataPath;
        }


        if (!Directory.Exists(Application.persistentDataPath + "/" + "worldsLoad"))
        {
            Directory.CreateDirectory(datapath + "/" + "worldsLoad");
            worldsdataPath = datapath + "/" + "worldsLoad";
        }

        if (!Directory.Exists(datapath + "/" + "world"))
        {
            Directory.CreateDirectory(datapath + "/" + "world");
            worldPath = datapath + "/" + "world";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
