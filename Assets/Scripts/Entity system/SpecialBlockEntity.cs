using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class SpecialBlockEntity : ObjectEntityBehaviour
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

    public delegate void Exceuted();
    public Exceuted exceuted { get; protected set; }


    public virtual void Exceute(Action act)
    {
        Action At = act;
        exceuted = new Exceuted(At);
        exceuted.Invoke();
    }

   

}
