using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public abstract class EntityAI : MonoBehaviour
{
    public abstract float health { get; protected set; } 
    public abstract float hunger { get; protected set; }
    public abstract int AttackPower { get; protected set; }
    public abstract float speed { get; protected set; }
    public abstract float RunSpeed { get; protected set; }
    public abstract Vector2 Position2D { get; protected set; }
    public abstract Vector3 Position3D { get; protected set; }
    public abstract Item ItemToDrop { get; set; }
    public abstract EntityAIState State { get; protected set; }

    public virtual void Drop()
    {
        GameObject go = Instantiate(new GameObject(ItemToDrop.name), Player.instance.transform.position, Quaternion.identity);
        go.AddComponent<PolygonCollider2D>();
        go.GetComponent<PolygonCollider2D>().isTrigger = true;
        go.AddComponent<SpriteRenderer>();
        go.GetComponent<SpriteRenderer>().sprite = ItemToDrop.Image;

        ItemObject io = go.AddComponent<ItemObject>();
        io.item = ItemToDrop;
    }


    public abstract void PositiveXMovement(int x);
    public abstract void PositiveYMovement(int y);
    public abstract void PositiveYXMovement(int x, int y);
    public abstract void NegativeXMovement(int x);
    public abstract void NegativeYMovement(int y);
    public abstract void NegativeYXMovement(int x, int y);

    public abstract void AI();

    public abstract void SetState(EntityAIState state);

    [Obsolete]
    public virtual void Follow(GameObject target)
    {
        Vector3 target3DPos = target.gameObject.transform.position;

        
        Vector3 curentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if(curentPos != target3DPos)
        {
            gameObject.GetComponent<Rigidbody2D>().MovePosition(curentPos + target3DPos * Time.deltaTime);
        }

        if(curentPos == target3DPos)
        {
            SetState(EntityAIState.Attacking);
        }

    }

    [Obsolete]
    public virtual void Attack(GameObject target)
    {
        Vector3 target3DPos = target.gameObject.transform.position;

        Position3D = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Vector3 curentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);

        if (target.name == "Player")
        {
            Player.instance.Health -= AttackPower;
        }

        if (curentPos != target3DPos)
        {
            SetState(EntityAIState.Following);
        }

    }


    /// <summary>
    /// Returns wether the object is in view of the camera
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    public virtual bool IsVisible(Camera c)
    {

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = gameObject.transform.position - new Vector3(-0.5f, -0.5f, 0);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < -2)
                return false;
        }
        return true;
    }

    /// <summary>
    /// Returns wether the position is in view of the camera
    /// </summary>
    /// <param name="c"></param>
    /// <param name="pos"></param>
    /// <returns></returns>
    public virtual bool IsVisible(Camera c, Vector3 pos)
    {

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point =  new Vector3(pos.x, pos.y, pos.z);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < -2)
                return false;
        }
        return true;
    }

    public virtual bool IsVisible(Camera c, Vector2 pos)
    {

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = new Vector3(pos.x, pos.y);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < -2)
                return false;
        }
        return true;
    }

    public virtual bool IsVisible(Camera c, GameObject obj)
    {

        var planes = GeometryUtility.CalculateFrustumPlanes(c);
        var point = obj.transform.position - new Vector3(-0.5f, -0.5f, 0);
        foreach (var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < -2)
                return false;
        }
        return true;
    }

    public virtual void Optimise(Camera cam)
    {
        gameObject.AddComponent<OptimiseEntityAI>();
        gameObject.GetComponent<OptimiseEntityAI>().main = cam;
        gameObject.GetComponent<OptimiseEntityAI>().ai = this;
    }


    public abstract Tile CheckTile(float x, float y);

    private void Update()
    {
        AI();
    }

   

}

public enum EntityAIState
{ 
    Roaming = 0,
    IDLE = 1,
    Attacking = 2,
    Following = 3,
    Eating = 4,
    Healing = 5,
    sleep = 6
}

