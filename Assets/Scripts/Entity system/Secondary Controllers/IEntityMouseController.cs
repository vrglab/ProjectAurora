using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[Obsolete("Curently this class is not fuctional")]
public struct IEntityMouseController
{

   public GameObject Object { get; set; }

    private delegate void OnMouseOver();
    private delegate void OnMouseLeftClickPressed();
    private delegate void OnMouseRightClickPressed();
    private delegate void OnMouseMidleClickPressed();
    private delegate void OnMouseMidleClickHeld();
    private delegate void OnMouseRightClickHeld();
    private delegate void OnMouseLeftClickHeld();

    OnMouseOver MIO;
    OnMouseLeftClickPressed MILP;
    OnMouseRightClickPressed MIRP;
    OnMouseMidleClickPressed MIMP;
    OnMouseLeftClickHeld MILH;
    OnMouseRightClickHeld MIRH;
    OnMouseMidleClickHeld MIMH;

    public void Awake(GameObject obj)
    {
        setObject(obj);
    }


    public void Update()
    {
        if (IsMouseover() == true)
        {
            if(MIO != null)
            {
                MIO.Invoke();
            }
           
            if (Input.GetMouseButtonDown(0))
            {
                MILP.Invoke();
            }
            else if (Input.GetMouseButtonDown(1))
            {
                MIRP.Invoke();
            }
            else if (Input.GetMouseButtonDown(2))
            {
                MIMP.Invoke();
            }
            if (Input.GetMouseButton(0))
            {
                MILH.Invoke();
            }
            else if (Input.GetMouseButton(1))
            {
                MIRH.Invoke();
            }
            else if (Input.GetMouseButton(2))
            {
                MIMH.Invoke();
            }
        }
    }

    public bool IsMouseover()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            if(hit.Equals(Object))
            {
                
                return true;
            }
            return true;
        }
        else
        {
            return false;
        }

       
    }

    
    public void  setObject(GameObject obj)
    {
        Object = obj;
    }

    public void DoThisOnMouseOver(Action act)
    {
        
        MIO = new OnMouseOver(delegate
        {
            act.Invoke();
        });


    }

    public  void DoThisOnMouseLeftclickPressed(Action act)
    {
        
        MILP = new OnMouseLeftClickPressed(delegate
        {
            act.Invoke();
        });


    }

    public  void DoThisOnMouseRighttclickPressed(Action act)
    {
        
        MIRP = new OnMouseRightClickPressed(delegate
        {
            act.Invoke();
        });


    }

    public  void DoThisOnMouseMidleclickPressed(Action act)
    {
        
        MIMP = new OnMouseMidleClickPressed(delegate
        {
            act.Invoke();
        });


    }

    public void DoThisOnMouseLeftclickHeld(Action act)
    {
        
        MILP = new OnMouseLeftClickPressed(delegate
        {
            act.Invoke();
        });


    }

    public void DoThisOnMouseRighttclickHeld(Action act)
    {
        MIRP = new OnMouseRightClickPressed(delegate
        {
            act.Invoke();
        });


    }

    public void DoThisOnMouseMidleclickHeld(Action act)
    {
        MIMP = new OnMouseMidleClickPressed(delegate
        {
            act.Invoke();
        });
    }
}
