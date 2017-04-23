using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VomitController : MonoBehaviour {

    GameObject product;
	// Use this for initialization
	void Start () {
        product = null;
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Infectable")
        {
            other.gameObject.GetComponent<InfectionController>().Infect();
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
