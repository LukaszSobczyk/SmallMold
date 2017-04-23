using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitController : MonoBehaviour {

    
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Infectable")
        {
            if (Input.GetKeyDown(KeyCode.E))
                other.gameObject.GetComponent<InfectionController>().Infect();
            if (Input.GetKeyUp(KeyCode.E))
                other.gameObject.GetComponent<InfectionController>().StopInfecting();
        }

    }

}
