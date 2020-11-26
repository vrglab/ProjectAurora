using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using ProjectAurora.Diagnostics;


/*
 * This is the base class that every Block like tile has to follow
 */
public abstract class BlockEntity : ObjectEntityBehaviour
{
    public virtual TileBlockType tt { get; set; }
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

    public BlockEntity(int x, int y)
{
    this.x = x;
    this.y = y;

}
public BlockEntity(int x, int y, int z)
{
    this.x = x;
    this.y = y;
    this.z = z;

}
public BlockEntity(int x, int y, TileBlockType type)
{
    this.x = x;
    this.y = y;
    tt = type;

}


/*this two return the position of the block in both
 * 3D and 2D
 */
public virtual Vector2 Get2DPosition()
{
    Vector2 pos = new Vector2(transform.position.x,
        transform.position.y);
    return pos;
}

public virtual Vector3 Get3DPosition()
{
    Vector3 pos = new Vector3(transform.position.x,
        transform.position.y, transform.position.z);
    return pos;
}

/*Here are just a bunch of the Geter and seters of the class
 */
public virtual Rigidbody2D GetRigidbody()
{
    return go.GetComponent<Rigidbody2D>();
}

public virtual SpriteRenderer GetSpRenderer()
{
    return go.GetComponent<SpriteRenderer>();
}

public virtual void SetTiletype(TileBlockType type)
{
    this.tt = type;
}

public virtual Sprite GetSprite()
{
    return go.GetComponent<SpriteRenderer>().sprite;
}

public virtual TileBlockType GetTiletype()
{
    return tt;
}

public virtual void SetSprite(Sprite sprite)
{
    go.GetComponent<SpriteRenderer>().sprite = sprite;
}

public virtual Collider2D GetCollider()
{
    return go.GetComponent<Collider2D>();
}

public virtual void SetPos(Vector2 pos)
{
    if (go.GetComponent<Rigidbody2D>())
    {
        go.GetComponent<Rigidbody2D>().MovePosition(pos);
    }

}

public virtual void SetPos(Vector3 pos)
{
    if (go.GetComponent<Rigidbody2D>())
    {
        go.GetComponent<Rigidbody2D>().position = pos;
    }
}

public virtual bool IsBlockSolid()
{
    if (IsSolid)
    {
        return true;
    }
    else
    {
        return false;
    }
}


/*This broadcasts a massage to the unityconsole form the 
 * PA diagnostics Debug class
 */
protected void BroadCastToconsole(object massage)
{
    ProjectAurora.Diagnostics.Debug.log(massage);
}

protected void BroadCastToconsole(object massage, LogType lt)
{
    if (lt == LogType.Normal)
    {
        ProjectAurora.Diagnostics.Debug.log(massage);
    }
    if (lt == LogType.Warning)
    {
        ProjectAurora.Diagnostics.Debug.logWarning(massage);
    }
    if (lt == LogType.Error)
    {
        ProjectAurora.Diagnostics.Debug.logError(massage);
    }

}



}
   

  




public enum TileBlockType
{
    empty = 0,
    grass = 1,
    stone = 2,
    IronOre = 3,
    Tree = 4
}

public enum TileOreType
{
    empty = 0,
    grass = 1,
    stone = 2,
    Tree = 4
}

public enum LogType
{
    Normal,
    Log,
    Warning,
    Error,
    Exception,
    Assert
}
