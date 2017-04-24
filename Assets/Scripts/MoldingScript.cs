using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldingScript {

    public static void ChangeMold(Renderer rend, float treshold, float moldHeight)
    {
        rend.material.SetFloat("_MoldLevel", 0.99f);
        //rend.material.SetFloat("_Treshhold", 0.5f);
        rend.material.SetFloat("_MoldHeight", 2 * moldHeight);
        if(rend.material.GetFloat("_MoldHeight") >= 2)
        {
            rend.material.SetFloat("_MoldHeight", 2);
            if (rend.material.GetFloat("_Treshhold") >= 0)
            {
                rend.material.SetFloat("_Treshhold", rend.material.GetFloat("_Treshhold") - 0.1f * Time.deltaTime);
            }
        }
        Debug.Log("treshold" + rend.material.GetFloat("_Treshhold"));
        Debug.Log("height:" + rend.material.GetFloat("_MoldHeight"));
    }
}
