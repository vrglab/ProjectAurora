using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.SAL;
using System.IO;

public class SaveInv : MonoBehaviour
{

    int worldID;

    public World w;
    
    




    // Start is called before the first frame update
    void Start()
    {
        worldID = w.ID;
        Debug.Log(worldID.ToString());
        string Path = w.WorldSavePath;
        
        if(!Directory.Exists(Path + "/" + "ISI"))
        {
            if (Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path + "/" + "ISI");
            }
            else if(Path == null)
            {
                ProjectAurora.Diagnostics.Debug.log("Test envoirment ready");
            }
            else
            {
                Debug.LogError("World:" + Path + " does not exist.");
            }
        }


       
        
    }

    public void SaveInventory(Slot slot)
    {
        string path = w.WorldSavePath;

        SlotData sd = new SlotData(slot.id, slot.IsInUse, slot.item, slot.Item, slot.EmptyImage, slot.Stacknum);



        saveSystem.save("Slot-"+sd.id, path + "/" + "ISI", DirectoryManager.Savetype, true, sd);

        Debug.Log("Inventory data saved to: " + path + "/" + "ISI" + "inv.pa");
        
    }
    
}
