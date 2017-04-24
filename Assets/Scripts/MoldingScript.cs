using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldingScript {

    public static void ChangeMold(Renderer rend, float treshold, float moldHeight)
    {
        rend.material.SetFloat("_MoldLevel", 0.99f);
        rend.material.SetFloat("_Treshhold", treshold);
        rend.material.SetFloat("_MoldHeight", moldHeight);
    }
}
