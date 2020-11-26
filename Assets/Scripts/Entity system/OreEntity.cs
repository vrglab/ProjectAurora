using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OreEntity : ObjectEntityBehaviour
{
    public override TileObject to { get => base.to; set => base.to = value; }
    public override Rigidbody2D rg { get => base.rg; set => base.rg = value; }
    public override BoxCollider2D bc2 { get => base.bc2; set => base.bc2 = value; }
    public override GameObject go { get => base.go; set => base.go = value; }
    public override float x { get => base.x; set => base.x = value; }
    public override float y { get => base.y; set => base.y = value; }
    public override float z { get => base.z; set => base.z = value; }
    public override bool HasRidigdbody2D { get => base.HasRidigdbody2D; set => base.HasRidigdbody2D = value; }
    public override bool HasCollider2D { get => base.HasCollider2D; set => base.HasCollider2D = value; }
    public override bool HasSpriteRenderer { get => base.HasSpriteRenderer; set => base.HasSpriteRenderer = value; }
    public override bool IsSolid { get => base.IsSolid; set => base.IsSolid = value; }
    public override string Name { get => base.Name; set => base.Name = value; }

    public virtual TileOreType oreType { get; set; }
    public virtual MinigType MiningType { get; set; }

    public virtual float Resitance { get; set; }

    public virtual Item ItemToDrop { get; set; }


    public virtual bool IsInBounds { get; set; } = false;



    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            IsInBounds = true;
        }
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            IsInBounds = false;
        }
    }

    public virtual void OnMouseOver()
    {
        if(Input.GetMouseButtonDown((int)MouseInputType.LeftClick))
        {
            print("Mouse pressed");
            if (IsInBounds == true)
            {
                print("Doing domwthing within bounds");
                if (Player.instance.Minetype == MiningType)
                {
                    Resitance -= (int)Player.instance.Minetype;
                }
                else
                {
                    print("None matching minig type");
                }
                
            
            }
        }
    }


    public virtual void Update()
    {
        if(Resitance <= 0)
        {
            Drop(ItemToDrop);
        }
    }

    public virtual void Drop(Item item)
    {
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
    }

}

