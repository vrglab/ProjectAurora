using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Diagnostics;


public class Inventory : MonoBehaviour
{
   

    public Slot[] slots;

    public Slot[] slotBar;

    public SaveInv si;

    public static Inventory instance;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogError("There are more than one inventory",gameObject);
        }
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].id = i;
        }
    }

    public void AddItemToSlot(Item item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            
            if(slots[i].IsInUse == true && item.Stackable == true && item.Name == slots[i].item.Name && slots[i].Stacknum != item.StackLimit)
            {

                    slots[i].AddItemToStack();

                try
                {
                    save(slots[i]);
                }
                catch (NullReferenceException)
                {

                    return;
                }
               

                return;
            }
            else if (slots[i].IsInUse == false)
            {
                slots[i].AddItem(item);
                try
                {
                    save(slots[i]);
                }
                catch (NullReferenceException)
                {

                    return;
                }
                return;
            }
            else
            {
                ProjectAurora.Diagnostics.Debug.logError("Inventory is full");
            }

        }
    }

    public void save(Slot slot)
    {
        try
        {
            si.SaveInventory(slot);
        }
        catch (NullReferenceException)
        {
            return;
        }
      
    }


}

