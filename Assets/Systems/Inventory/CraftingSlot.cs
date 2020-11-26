using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.UI;
using UnityEngine;


public class CraftingSlot : MonoBehaviour
{



    public Image ItemImage;

    public Text Stacktext;

    public int Stacknum;

    public Sprite EmptyImage;


    public GameObject Item;

    public Item item { get; set; }

    public bool IsInUse { get; protected set; }

    public int id;


    public void AddItem(Item item)
    {
        ItemImage.sprite = item.Image;
        this.item = item;
        IsInUse = true;
       
    }

    public void AddItemToStack()
    {

        Stacknum += 1;
        if (Stacknum == 1)
        {
            Stacknum += 1;
        }
        Stacktext.text = Stacknum.ToString();


    }

    private void Update()
    {

    }





    public void RemoveItem(int amount)
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
            item = null;
            IsInUse = false;
          
        }
        else if (Stacknum > 0)
        {
            Stacknum -= 1;
            GameObject go = Instantiate(new GameObject(item.name), Player.instance.transform.position, Quaternion.identity);
            go.AddComponent<PolygonCollider2D>();
            go.GetComponent<PolygonCollider2D>().isTrigger = true;
            go.AddComponent<SpriteRenderer>();
            go.GetComponent<SpriteRenderer>().sprite = item.Image;

            ItemObject io = go.AddComponent<ItemObject>();
            io.item = item;
    
        }



    }

    private void OnMouseOver()
    {
        ProjectAurora.Diagnostics.Debug.log("Mouse is over slot: " + id);
        if (Input.GetKeyDown(KeyCode.Q))
        {

            //RemoveItem();

        }
    }
}
