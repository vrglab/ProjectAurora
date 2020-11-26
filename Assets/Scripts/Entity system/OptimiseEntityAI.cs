using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimiseEntityAI : MonoBehaviour
{
    public Camera main;
    public EntityAI ai;
   

    private void Update()
    {

        if (IsVisible(main) == true)
        {
            if (gameObject.GetComponent<SpriteRenderer>())
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (gameObject.GetComponent<Collider2D>())
            {
                gameObject.GetComponent<Collider2D>().enabled = true;
            }
           
                ai.enabled = true;
           

        }
        if (IsVisible(main) == false)
        {
            if (gameObject.GetComponent<SpriteRenderer>())
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (gameObject.GetComponent<Collider2D>())
            {
                gameObject.GetComponent<Collider2D>().enabled = false;
            }
            ai.enabled = false;
        }


    }


   


    bool IsVisible(Camera c)
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
}
