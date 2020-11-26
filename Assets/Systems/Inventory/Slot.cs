using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;



public class Slot : MonoBehaviour
{

    public Image ItemImage;

    public Text Stacktext;

    public int Stacknum;

    public Sprite EmptyImage;


    public GameObject Item;

    public Item item { get;  set; }

   public bool IsInUse { get; protected set; }

   public int id;
    

   public void AddItem(Item item)
   {
      ItemImage.sprite =  item.Image;
      this.item = item;
      IsInUse = true;
      Inventory.instance.save(this);
   }

    public void AddItemToStack()
    {

        Stacknum += 1;
        if(Stacknum == 1)
        {
            Stacknum += 1;
        }
        Stacktext.text = Stacknum.ToString();
        
        Inventory.instance.save(this);
    }

    private void Update()
    {
        
    }





    public void RemoveItem()
    {
        if (Stacknum == 0)
        {
            ItemImage.sprite = null;

            GameObject go = Instantiate(new GameObject(item.name), Player.instance.transform.position, Quaternion.identity);
            go.AddComponent<PolygonCollider2D>();
            go.GetComponent<PolygonCollider2D>().isTrigger = true;
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = item.Image;

            ItemObject io = go.AddComponent<ItemObject>();
            io.item = item;
            io.item.Craftable = item.Craftable;
            io.item.Discription = item.Discription;
            io.item.Name = item.Name;
            io.item.Stackable = item.Stackable;
            io.item.StackLimit = item.StackLimit;
            io.item.Type = item.Type;
            io.item.Id = item.Id;
            item = null;
            IsInUse = false;
            Inventory.instance.save(this);
        }
        else if(Stacknum > 0)
        {
            Stacknum -= 1;
            GameObject go = Instantiate(new GameObject(item.name), Player.instance.transform.position, Quaternion.identity);
            go.AddComponent<PolygonCollider2D>();
            go.GetComponent<PolygonCollider2D>().isTrigger = true;
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = item.Image;

            ItemObject io = go.AddComponent<ItemObject>();
            io.item = item;
            io.item.Craftable = item.Craftable;
            io.item.Discription = item.Discription;
            io.item.Name = item.Name;
            io.item.Stackable = item.Stackable;
            io.item.StackLimit = item.StackLimit;
            io.item.Type = item.Type;
            io.item.Id = item.Id;
            Inventory.instance.save(this);
        }
        else if(Stacknum < 0)
        {
            throw new IndexOutOfRangeException("There are minus amount of items in slot: " + id.ToString());
        }
        


    }

    private void OnMouseOver()
    {
        Debug.Log("Mouse is over slot: " + id);
        if (Input.GetKeyDown(KeyCode.Q))
        {

            RemoveItem();
            
        }
    }
}


[Serializable]
public class SlotData
{

    public int stacknum;

    public Sprite emptyImage;

    public GameObject item;

    public Item tem;

    public bool IsInUse;

    public int id;

    public SlotData(int id, bool IsInUse, Item tem, GameObject item, Sprite emptyImage, int stacknum)
    {

    }

}

