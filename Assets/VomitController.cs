using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitController : MonoBehaviour {

    public ParticleSystem particle;
    
    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.tag == "Infectable")
        {
            if (Input.GetKey(KeyCode.E))
            {
                other.gameObject.GetComponent<InfectionController>().Infect();
                particle.Play();
            }

            if (Input.GetKeyUp(KeyCode.E))
            {
                other.gameObject.GetComponent<InfectionController>().StopInfecting();
                particle.Stop();
            }

        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Infectable")
        {
            other.gameObject.GetComponent<InfectionController>().StopInfecting();
        }
    }

}
