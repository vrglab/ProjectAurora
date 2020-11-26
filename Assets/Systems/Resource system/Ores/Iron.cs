using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Iron : Ore
{
    public Item item;

    private int resitance = 2;

    public Iron(ItemObject todrop, int resitance, MinigType mineType) : base(todrop, resitance, mineType)
    {

    }

    public override void Drop()
    {
        GameObject game = new GameObject();
        GameObject go = Instantiate(game,gameObject.transform.position,Quaternion.identity);
        go.AddComponent<ItemObject>();
        go.AddComponent<SpriteRenderer>();
        go.AddComponent<PolygonCollider2D>();
        ItemTodrop = go.GetComponent<ItemObject>();
        ItemTodrop.item = item;
        go.name = item.Name;
        go.GetComponent<SpriteRenderer>().sprite = item.Image;
        go.GetComponent<PolygonCollider2D>().isTrigger = true;


        Destroy(obj: gameObject);
        
    }

    public override ItemObject GetItemToDrop()
    {
        return base.GetItemToDrop();
    }

    public override MinigType GetMinieType()
    {
        return MinigType.Pickaxe;
    }

    public override int GetResitance()
    {
        return resitance;
    }



    private void OnMouseDown()
    {

            Debug.Log("Mous is pressed on me", gameObject);
            
            if (GetResitance() != 0)
            {
                resitance -= 1;
            }
            else if (GetResitance() == 0)
            {
                Drop();
            }

    }


}
