using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IronOre : OreEntity
{
    public override TileObject to { get => base.to; set => base.to = value; }
    public override Rigidbody2D rg { get => gameObject.GetComponent<Rigidbody2D>(); set => base.rg = value; }
    public override BoxCollider2D bc2 { get => gameObject.GetComponent<BoxCollider2D>(); set => base.bc2 = value; }
    public override GameObject go { get => base.go; set => base.go = value; }
    public override float x { get => base.x; set => base.x = value; }
    public override float y { get => base.y; set => base.y = value; }
    public override float z { get => base.z; set => base.z = value; }
    public override bool HasRidigdbody2D { get => true; set => base.HasRidigdbody2D = value; }
    public override bool HasCollider2D { get => true; set => base.HasCollider2D = value; }
    public override bool HasSpriteRenderer { get => true; set => base.HasSpriteRenderer = value; }
    public override bool IsSolid { get => base.IsSolid; set => base.IsSolid = value; }
    public override string Name { get => "Iron Ore"; set => base.Name = value; }
    public override TileOreType oreType { get => TileOreType.stone; set => base.oreType = value; }
    public override MinigType MiningType { get => MinigType.Pickaxe; set => base.MiningType = value; }
    public override float Resitance { get => 3.8f; set => base.Resitance = value; }
    public override Item ItemToDrop { get => Item; set => base.ItemToDrop = value; }
    public override bool IsInBounds { get => base.IsInBounds; set => base.IsInBounds = value; }

    public Item Item;


    public override void Drop(Item item)
    {
        base.Drop(item);
    }

    public override void OnMouseOver()
    {
        base.OnMouseOver();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    public override void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
    }

    public override void Update()
    {
        base.Update();
        print(IsInBounds);
    }
}
