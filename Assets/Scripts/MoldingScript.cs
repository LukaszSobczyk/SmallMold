using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldingScript {

    public static void ChangeMold(Renderer rend, float treshold, float moldHeight)
    {
        //Debug.Log("treshold: " + treshold + "_Treshold: " + rend.material.GetFloat("_Treshhold"));
        rend.material.SetFloat("_MoldLevel", 0.99f);
        //if(rend.material.GetFloat("_Treshhold") < 1)
        //{
        //    rend.material.SetFloat("_Treshhold", 1);
        //}
        //rend.material.SetFloat("_Treshhold", 0.5f);
        rend.material.SetFloat("_MoldHeight", Mathf.Lerp(rend.material.GetFloat("_MoldHeight"), 2, Time.deltaTime * moldHeight));
        if (rend.material.GetFloat("_MoldHeight") >= 2)
        {
            rend.material.SetFloat("_MoldHeight", 2);
        }
        if (moldHeight >= 0.5)
        {
            //rend.material.SetFloat("_MoldHeight", 2);
            
            if (rend.material.GetFloat("_Treshhold") >= 0)
            {
                rend.material.SetFloat("_Treshhold", Mathf.Lerp(rend.material.GetFloat("_Treshhold"), 0, Time.deltaTime * treshold * 0.5f));
            }
        }
        //Debug.Log(treshold);
        //Debug.Log("_Treshhold: " + rend.material.GetFloat("_Treshhold") + "\n _MoldHeight:" + rend.material.GetFloat("_MoldHeight"));
    }
}
